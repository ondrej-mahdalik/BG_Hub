using BrokenGrenade.Data;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Seeds
{
    public static class MissionSeeds
    {
        public static readonly Mission Mission1 = new("Perkusní dobrodružství", "https://google.com", DateTime.Now)
        {
            MissionId = Guid.Parse("ebe540ab-7fdc-4ace-80b8-2a22347dfac6"),
            AuthorId = UserSeeds.TestUser.Id
        };

        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Mission>().HasData(Mission1);
        }
    }
}
