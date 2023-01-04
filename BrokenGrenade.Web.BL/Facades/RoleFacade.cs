using System.Security.Claims;
using AutoMapper;
using BrokenGrenade.Common.Models;
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
    
    public RoleFacade(IMapper mapper, RoleManager<RoleEntity> roleManager)
    {
        _mapper = mapper;
        _roleManager = roleManager;
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

    public async Task<List<RoleModel>> GetAsync()
    {
        var entities = await _roleManager.Roles.ToListAsync();
        var models = new List<RoleModel>();

        foreach (var entity in entities)
        {
            var model = await MapRoleAsync(entity);
            models.Add(model);
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

        model.CreateMissions = permissions.Contains(PermissionTypes.CreateMissions);
        model.ManageMissions = permissions.Contains(PermissionTypes.ManageMissions);
        model.ManageUsers = permissions.Contains(PermissionTypes.ManageUsers);
        model.ManageRoles = permissions.Contains(PermissionTypes.ManageRoles);
        model.ManageMissionTypes = permissions.Contains(PermissionTypes.ManageMissionTypes);
        model.ManageModpackTypes = permissions.Contains(PermissionTypes.ManageModpackTypes);

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
        var permissions = new List<string>();
        if (model.CreateMissions)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.CreateMissions));
        
        if (model.ManageMissions)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageMissions));
        
        if (model.ManageUsers)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageUsers));
        
        if (model.ManageRoles)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageRoles));
        
        if (model.ManageMissionTypes)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageMissionTypes));
        
        if (model.ManageModpackTypes)
            await _roleManager.AddClaimAsync(entity, new Claim("permission", PermissionTypes.ManageModpackTypes));
    }
}