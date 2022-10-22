using BrokenGrenade.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Seeds
{
    public static class UserSeeds
    {
        public static readonly User TestUser = new User()
        {
            Id = "80cb8634-60e0-4e1c-a0ad-9d0a77371c96",
            Email = "ondrej.mahdalik@gmail.com",
            EmailConfirmed = true,
            UserName = "ondrej.mahdalik@gmail.com"
        };

        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>() ?? throw new Exception();
            await userManager.CreateAsync(TestUser, "Password123$");
        }
    }

    
}
