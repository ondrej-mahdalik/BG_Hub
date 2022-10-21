using BrokenGrenade.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Seeds
{
    public static class UserSeeds
    {
        public static readonly User TestUser = new User()
        {
            Id = Guid.Parse("80cb8634-60e0-4e1c-a0ad-9d0a77371c96"),
            Email = "ondrej.mahdalik@gmail.com",
            EmailConfirmed = true,
            UserName = "ondrej",
            PasswordHash = new PasswordHasher<User>().HashPassword(null, "pass")
        };

        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(TestUser);
        }
    }

    
}
