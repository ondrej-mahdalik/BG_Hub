using System.Security.Claims;
using BrokenGrenade.Common.Permissions;
using BrokenGrenade.Web.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BrokenGrenade.Web.DAL.Seeds;

public static class RoleSeeds
{
    public static readonly RoleEntity Administrator = new("Administrátor")
    {
        Id = new Guid("8848CA4E-4B0A-4705-95CA-DB0E9B3B394C")
    };

    public static async Task Seed(RoleManager<RoleEntity> roleManager, bool onlyDefaultRole = false)
    {
        await roleManager.CreateAsync(Administrator);

        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.CreateMissions));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageUsers));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageRoles));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageMissions));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageMissionTypes));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageModpackTypes));

        if (onlyDefaultRole)
            return;
        
        // TODO Add more roles
    }
}