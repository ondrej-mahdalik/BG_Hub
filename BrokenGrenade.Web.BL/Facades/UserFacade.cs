using System.Text;
using System.Text.Encodings.Web;
using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Common.Models.Filters;
using BrokenGrenade.Web.Common.Facades;
using BrokenGrenade.Web.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BrokenGrenade.Web.BL.Facades;

public class UserFacade : IAppFacade
{
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;
    private readonly ApplicationFacade _applicationFacade;
    private readonly PunishmentFacade _punishmentFacade;
    private readonly RoleFacade _roleFacade;
    private readonly MissionFacade _missionFacade;
    private readonly DiscordWebhookSender _discordWebhookSender;

    public UserFacade(UserManager<UserEntity> userManager,
        IMapper mapper, IEmailSender emailSender,
        IConfiguration configuration,
        ApplicationFacade applicationFacade,
        PunishmentFacade punishmentFacade, RoleFacade roleFacade, MissionFacade missionFacade, DiscordWebhookSender discordWebhookSender)
    {
        _userManager = userManager;
        _mapper = mapper;
        _emailSender = emailSender;
        _configuration = configuration;
        _applicationFacade = applicationFacade;
        _punishmentFacade = punishmentFacade;
        _roleFacade = roleFacade;
        _missionFacade = missionFacade;
        _discordWebhookSender = discordWebhookSender;
    }

    public async Task DeleteAsync(Guid id, string? reason = null)
    {
        var entity = await _userManager.FindByIdAsync(id.ToString());
        if (entity is null)
            return;
        
        // Send management notification
        await _discordWebhookSender.SendManagementMessageAsync("Odstraněný uživatel", $"Uživatel {entity.Nickname} byl odstraněn." + (!string.IsNullOrWhiteSpace(reason) ? $" Důvod: {reason}" : ""));
        
        // Update application
        var application = await _applicationFacade.GetByUserAsync(id);
        if (application is not null)
        {
            application.UserId = null;
            await _applicationFacade.SaveAsync(application);
        }
        
        // Delete punishments
        var punishments = await _punishmentFacade.GetByUserAsync(id);
        foreach (var punishment in punishments)
            await _punishmentFacade.DeleteAsync(punishment.Id);
        
        await _userManager.DeleteAsync(entity);
    }

    public async Task DeleteAsync(UserModel user, string? reason = null)
    {
        await DeleteAsync(user.Id, reason);
    }
    
    public async Task<UserModel?> GetAsync(Guid id)
    {
        var entity = await _userManager.FindByIdAsync(id.ToString());
        if (entity is null)
            return null;
        
        var roles = await _roleFacade.GetByUserAsync(id);
        
        var model = _mapper.Map<UserModel>(entity);
        model.Roles = roles;
        
        return model;
    }

    public async Task<List<UserModel>> GetPaginatedAsync(UserFilterModel filter, int page)
    {
        var query = GetFiltered(filter);
        var entities = await query
            .OrderBy(x => x.Nickname)
            .ThenBy(x => x.Id)
            .Skip(page * 10)
            .Take(10)
            .ToListAsync();
        
        var users = _mapper.Map<List<UserModel>>(entities);
        foreach (var user in users)
        {
            var roles = await _roleFacade.GetByUserAsync(user.Id);
            user.Roles = roles;
        }

        return users;
    }

    public async Task<List<UserModel>> GetAsync(UserFilterModel filter)
    {
        var entities = await GetFiltered(filter).ToListAsync();
        var users = _mapper.Map<List<UserModel>>(entities);
        foreach (var user in users)
        {
            var roles = await _roleFacade.GetByUserAsync(user.Id);
            user.Roles = roles;
        }
        
        return users;
    }
    
    public async Task<List<UserModel>> GetAsync()
    {
        var entities = await _userManager.Users
            .ToListAsync();

        var users = _mapper.Map<List<UserModel>>(entities);
        foreach (var user in users)
        {
            var roles = await _roleFacade.GetByUserAsync(user.Id);
            user.Roles = roles;
        }

        return users;
    }

    public async Task<List<UserModel>> GetWithoutNavigationPropertiesAsync()
    {
        var query = _userManager.Users;
        return await _mapper.ProjectTo<UserModel>(query).ToListAsync().ConfigureAwait(false);
    }
    
    public async Task<int> GetCountAsync(UserFilterModel filter)
    {
        return await GetFiltered(filter).CountAsync();
    }

    public async Task<string> GetNicknameAsync(Guid userId)
    {
        return await _userManager.Users.Where(x => x.Id == userId).Select(x => x.Nickname).FirstAsync();
    }
    
    public async Task UpdateAsync(UserModel user)
    {
        // Update user
        var originalEntity = await _userManager.FindByIdAsync(user.Id.ToString());
        if (originalEntity is null)
        {
            throw new InvalidOperationException("User not found");
        }

        var updatedEntity = _mapper.Map(user, originalEntity);
        await _userManager.UpdateAsync(updatedEntity);

        // Update roles
        await AssignRolesAsync(user);
    }
    
    public async Task CreateAsync(UserModel user)
    {
        // Create new account
        var entity = _mapper.Map<UserEntity>(user);
        if (entity is null)
        {
            throw new InvalidOperationException("Failed to map user");
        }

        entity.EmailConfirmed = true;
        // entity.UserName = user.Email;

        var password = "Temp_" + Guid.NewGuid().ToString("N");

        var result = await _userManager.CreateAsync(entity, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(result.Errors.First().Description);
        }

        // Assign roles
        await AssignRolesAsync(user);

        // Generate and send password reset token
        var code = await _userManager.GeneratePasswordResetTokenAsync(entity);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var userId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(entity.Id.ToString()));
        var callbackUrl = $"{_configuration["BaseAddress"]}/Identity/Account/ResetPassword?code={code}&userId={userId}";

        await _emailSender.SendEmailAsync(user.Email, "Vytvoření Nového Účtu",
            $"Dobrý den,<br><br>Váš účet byl úspěšně vytvořen.<br>Vaše uživatelské jméno: {entity.Email}" +
            $"<br>Pro nastavení hesla klikněte na <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>tento odkaz</a>." +
            "<br><br>S pozdravem,<br><br>Broken Grenade");
    }
    
    public async Task ResetPasswordAsync(string email)
    {
        var entity = await _userManager.FindByEmailAsync(email);
        if (entity is null)
        {
            throw new InvalidOperationException("User not found");
        }

        var code = await _userManager.GeneratePasswordResetTokenAsync(entity);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var userId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(entity.Id.ToString()));
        var callbackUrl = $"{_configuration["BaseAddress"]}/Identity/Account/ResetPassword?code={code}&userId={userId}";

        await _emailSender.SendEmailAsync(email, "Obnovení Hesla",
            $"Dobrý den,<br><br>Pro resetování hesla klikněte na <a href='{callbackUrl}'>tento odkaz</a>." +
            "<br><br>S pozdravem,<br><br>Broken Grenade");
    }

    private async Task AssignRolesAsync(UserModel user)
    {
        var entity = await _userManager.FindByIdAsync(user.Id.ToString());
        if (entity is null)
            throw new InvalidOperationException("User not found");
        
        var allRoles = await _userManager.GetRolesAsync(entity);
        await _userManager.RemoveFromRolesAsync(entity, allRoles);
        await _userManager.AddToRolesAsync(entity, user.Roles.Select(x => x.Name));
    }

    public async Task<bool> CanUserEditMissionAsync(Guid userId, Guid missionId)
    {
        var user = await GetAsync(userId);
        var mission = await _missionFacade.GetAsync(missionId);
        
        if (user is null || mission is null)
            return false;
        
        return mission.CreatorId == userId || user.Roles.Any(x => x.ManageMissions);

    }

    private IQueryable<UserEntity> GetFiltered(UserFilterModel? filter)
    {
        var query = _userManager.Users
            .Include(x => x.UserRoles)
            .AsQueryable();
        
        if (filter is null)
            return query;

        if (!string.IsNullOrEmpty(filter.Nickname))
            query = query.Where(x => x.Nickname.ToLower().Contains(filter.Nickname.ToLower()));

        if (!string.IsNullOrEmpty(filter.Email))
            query = query.Where(x => x.Email != null && x.Email.ToLower().Contains(filter.Email.ToLower()));

        if (filter.RoleId is not null)
            query = query.Where(x => x.UserRoles.Any(y => y.RoleId == filter.RoleId));

        return query;
    }
}