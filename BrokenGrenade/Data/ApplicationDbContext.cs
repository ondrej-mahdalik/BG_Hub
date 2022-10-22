using BrokenGrenade.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Mission> Missions => Set<Mission>();
        public DbSet<MissionCategory> Categories => Set<MissionCategory>();
        public DbSet<ModpackType> Modpacks => Set<ModpackType>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override async void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(i => i.Missions)
                .WithOne(i => i.Author)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<MissionCategory>()
                .HasMany(i => i.Missions)
                .WithOne(i => i.Category)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ModpackType>()
                .HasMany(i => i.Missions)
                .WithOne(i => i.ModpackType)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}