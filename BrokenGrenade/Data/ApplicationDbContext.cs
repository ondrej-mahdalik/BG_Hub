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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.Missions)
                .WithOne(u => u.Author)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MissionCategory>()
                .HasMany(i => i.Missions)
                .WithOne(i => i.Category)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed demo data
            UserSeeds.Seed(builder);
            MissionSeeds.Seed(builder);
        }
    }
}