using System.Security.Claims;
using BrokenGrenade.Data;
using Microsoft.AspNetCore.Identity;

namespace BrokenGrenade.Seeds;

public static class RoleSeeds
{
    public static readonly IdentityRole MissionMaker = new("Mission Maker");
    public static readonly IdentityRole PlatoonLead = new("Platoon Lead");

    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>() ?? throw new Exception();
        
        await roleManager.CreateAsync(MissionMaker);
        await roleManager.AddClaimAsync(MissionMaker, new Claim("CreateMissions", "true"));

        await roleManager.CreateAsync(PlatoonLead);
        await roleManager.AddClaimAsync(PlatoonLead, new Claim("ViewStaffMenu", "true"));
        await roleManager.AddClaimAsync(PlatoonLead, new Claim("CreateMissions", "true"));
        await roleManager.AddClaimAsync(PlatoonLead, new Claim("ManageMissions", "true"));
        await roleManager.AddClaimAsync(PlatoonLead, new Claim("ManageUsers", "true"));
    }
}