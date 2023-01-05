using BrokenGrenade.Web.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BrokenGrenade.Web.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity Administrator = new("Administrátor", "admin@brokengrenade.cz")
    {
        Id = new Guid("1B11597A-2623-486E-8349-1EFE4F4F5A18"),
        EmailConfirmed = true
    };
    
    public static readonly UserEntity Borek = new("Bořek", "borek.stavitel@gmail.com")
    {
        Id = new Guid("22799841-3AB8-4EB2-B3BE-09BB55896D5D"),
        EmailConfirmed = true
    };

    public static async Task SeedAsync(UserManager<UserEntity> userManager, bool onlyDefaultAccount = false)
    {
        await userManager.CreateAsync(Administrator, "Pass123$");
        await userManager.AddToRoleAsync(Administrator, RoleSeeds.Administrator.Name!);
        if (onlyDefaultAccount)
            return;
        
        await userManager.CreateAsync(Borek, "Pass123$");
        await userManager.AddToRoleAsync(Borek, RoleSeeds.MissionMaker.Name!);
        // TODO: Add more users
    }
}