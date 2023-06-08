using System.Security.Claims;
using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Common.Models.Filters;
using BrokenGrenade.Common.Permissions;
using BrokenGrenade.Web.Common.Facades;
using BrokenGrenade.Web.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.BL.Facades;

public class RoleFacade : IAppFacade
{
    private readonly IMapper _mapper;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly UserManager<UserEntity> _userManager;

    public RoleFacade(IMapper mapper, RoleManager<RoleEntity> roleManager, UserManager<UserEntity> userManager)
    {
        _mapper = mapper;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<List<RoleModel>> GetAsync(RoleFilterModel filter)
    {
        var query = _roleManager.Roles.OrderByDescending(x => x.Order).AsQueryable();
        if (filter.RoleName is not null)
            query = query.Where(x => x.Name != null && x.Name.ToLower().Contains(filter.RoleName.ToLower()));

        var entities = await query.ToListAsync();
        var models = new List<RoleModel>();
        foreach (var entity in entities)
            models.Add(await MapRoleAsync(entity));
        
        return models;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _roleManager.FindByIdAsync(id.ToString());
        if (entity is not null)
            await _roleManager.DeleteAsync(entity);
    }

    public async Task DeleteAsync(RoleModel role)
    {
        await DeleteAsync(role.Id);
    }

    public async Task<RoleModel?> GetAsync(Guid id)
    {
        var entity = await _roleManager.FindByIdAsync(id.ToString());
        if (entity is null)
            return null;
        
        return await MapRoleAsync(entity);
    }
    
    public async Task<RoleModel?> GetAsync(string name)
    {
        var entity = await _roleManager.FindByNameAsync(name);
        if (entity is null)
            return null;
        
        return await MapRoleAsync(entity);
    }

    public async Task<List<RoleModel>> GetAsync()
    {
        var entities = await _roleManager.Roles.ToListAsync();
        var models = new List<RoleModel>();

        foreach (var entity in entities)
            models.Add(await MapRoleAsync(entity));
        
        return models;
    }

    public async Task<List<RoleModel>> GetByUserAsync(Guid userId)
    {
        var userEntity = await _userManager.FindByIdAsync(userId.ToString());
        if (userEntity is null)
            return new List<RoleModel>();
        
        var roleNames = await _userManager.GetRolesAsync(userEntity);
        var models = new List<RoleModel>();

        foreach (var roleName in roleNames)
        {
            var entity = await _roleManager.FindByNameAsync(roleName);
            if (entity is not null)
                models.Add(await MapRoleAsync(entity));
        }

        return models;
    }

    public async Task UpdateAsync(RoleModel role)
    {
        var originalEntity = await _roleManager.FindByIdAsync(role.Id.ToString());
        if (originalEntity is null)
        {
            throw new InvalidOperationException("Role not found");
        }

        var updatedEntity = _mapper.Map(role, originalEntity);
        await _roleManager.UpdateAsync(updatedEntity);

        await SetRolePermissionsAsync(role);
    }

    public async Task CreateAsync(RoleModel role)
    {
        var entity = _mapper.Map<RoleEntity>(role);
        await _roleManager.CreateAsync(entity);

        await SetRolePermissionsAsync(role);
    }
    
    private async Task<RoleModel> MapRoleAsync(RoleEntity entity)
    {
        var model = _mapper.Map<RoleModel>(entity);
        var claims = await _roleManager.GetClaimsAsync(entity);
        var permissions = claims.Where(x => x.Type == "permission").Select(x => x.Value).ToList();
        if (entity.Name != null)
        {
            var users = await _userManager.GetUsersInRoleAsync(entity.Name);
            model.UserCount = users.Count;
        }

        model.CreateMissions = permissions.Contains(PermissionTypes.CreateMissions);
        model.CreateTrainings = permissions.Contains(PermissionTypes.CreateTrainings);
        model.CreateArticles = permissions.Contains(PermissionTypes.CreateArticles);
        model.ManageMissions = permissions.Contains(PermissionTypes.ManageMissions);
        model.ManageTrainings = permissions.Contains(PermissionTypes.ManageTrainings);
        model.ManageArticles = permissions.Contains(PermissionTypes.ManageArticles);
        model.ManageUsers = permissions.Contains(PermissionTypes.ManageUsers);
        model.ManageRoles = permissions.Contains(PermissionTypes.ManageRoles);
        model.ManageMissionTypes = permissions.Contains(PermissionTypes.ManageMissionTypes);
        model.ManageModpackTypes = permissions.Contains(PermissionTypes.ManageModpackTypes);
        model.ManageApplications = permissions.Contains(PermissionTypes.ManageApplications);
        model.ManagePunishments = permissions.Contains(PermissionTypes.ManagePunishments);

        return model;
    }
    
    private async Task SetRolePermissionsAsync(RoleModel model)
    {
        var entity = await _roleManager.FindByIdAsync(model.Id.ToString());
        if (entity is null)
            return;
        
        // Remove old permissions
        var claims = await _roleManager.GetClaimsAsync(entity);
        var permissionClaims = claims.Where(x => x.Type == "permission");
        foreach (var claim in permissionClaims)
            await _roleManager.RemoveClaimAsync(entity, claim);
        
        // Add new permissions
        if (model.CreateMissions)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.CreateMissions));
        
        if (model.CreateTrainings)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.CreateTrainings));
        
        if (model.CreateArticles)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.CreateArticles));
        
        if (model.ManageMissions)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageMissions));
        
        if (model.ManageTrainings)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageTrainings));
        
        if (model.ManageArticles)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageArticles));
        
        if (model.ManageUsers)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageUsers));
        
        if (model.ManageRoles)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageRoles));
        
        if (model.ManageMissionTypes)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageMissionTypes));
        
        if (model.ManageModpackTypes)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageModpackTypes));
        
        if (model.ManageApplications)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageApplications));
        
        if (model.ManagePunishments)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManagePunishments));
    }
}