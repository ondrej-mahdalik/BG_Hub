using System.Text;
using System.Text.Encodings.Web;
using AutoMapper;
using BrokenGrenade.Common.Models;
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
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly UserManager<UserEntity> _userManager;
    
    public UserFacade(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IMapper mapper, IEmailSender emailSender, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _emailSender = emailSender;
        _configuration = configuration;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _userManager.FindByIdAsync(id.ToString());
        if (entity is null)
            return;
        

        await _userManager.DeleteAsync(entity);
    }

    public async Task DeleteAsync(UserModel user)
    {
        await DeleteAsync(user.Id);
    }
    
    public async Task<UserModel?> GetAsync(Guid id)
    {
        var entity = await _userManager.FindByIdAsync(id.ToString());
        if (entity is null)
            return null;
        
        var model = _mapper.Map<UserModel>(entity);
        await GetRolesAsync(model);
        return model;
    }
    
    public async Task<List<UserModel>> GetAsync()
    {
        var entities = await _userManager.Users
            .ToListAsync();

        var users = _mapper.Map<List<UserModel>>(entities);
        foreach (var user in users)
            await GetRolesAsync(user);

        return users;
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
        entity.UserName = user.Email;

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
    
    private async Task GetRolesAsync(UserModel user)
    {
        var entity = await _userManager.FindByIdAsync(user.Id.ToString());
        if (entity is null)
            throw new InvalidOperationException("User not found");
        
        var roles = await _userManager.GetRolesAsync(entity);
        user.Roles = _mapper.Map<List<RoleModel>>(roles.Select(x => _roleManager.FindByNameAsync(x).Result).ToList());
    }
    
    private async Task AssignRolesAsync(UserModel user)
    {
        if (user.Roles is null)
            return;
        
        var entity = await _userManager.FindByIdAsync(user.Id.ToString());
        if (entity is null)
            throw new InvalidOperationException("User not found");
        
        var allRoles = await _userManager.GetRolesAsync(entity);
        await _userManager.RemoveFromRolesAsync(entity, allRoles);
        await _userManager.AddToRolesAsync(entity, user.Roles.Select(x => x.Name));
    }
}