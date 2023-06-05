using System.Security.Claims;
using BrokenGrenade.Common.Permissions;
using BrokenGrenade.Web.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BrokenGrenade.Web.DAL.Seeds;

public static class RoleSeeds
{
    public static readonly RoleEntity Administrator = new("Administrátor")
    {
        Id = new Guid("8848CA4E-4B0A-4705-95CA-DB0E9B3B394C"),
        Order = 100
    };

    public static readonly RoleEntity MissionMaker = new("Mission Maker")
    {
        Id = new Guid("233BB63A-FABE-460B-817D-269DD81CDE64"),
        ColorHex = "#ff6c00",
        Order = 10
    };


    public static async Task SeedAsync(RoleManager<RoleEntity> roleManager, bool onlyDefaultRole = false)
    {
        await roleManager.CreateAsync(Administrator);
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.CreateMissions));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.CreateTrainings));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.CreateArticles));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageUsers));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageRoles));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageMissions));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageTrainings));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageArticles));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageMissionTypes));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageModpackTypes));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManageApplications));
        await roleManager.AddClaimAsync(Administrator, new Claim("permission", PermissionTypes.ManagePunishments));
        
        if (onlyDefaultRole)
            return;

        await roleManager.CreateAsync(MissionMaker);
        await roleManager.AddClaimAsync(MissionMaker, new Claim("permission", PermissionTypes.CreateMissions));
        // TODO Add more roles
    }
}