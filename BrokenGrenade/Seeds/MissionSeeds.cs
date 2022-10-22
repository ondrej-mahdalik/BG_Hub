using BrokenGrenade.Data;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Seeds
{
    public static class MissionSeeds
    {
        public static readonly Mission Mission1 = new("Perkusní dobrodružství", "https://google.com", DateTime.Now + TimeSpan.FromMinutes(30), DateTime.Now)
        {
            Id = Guid.Parse("ebe540ab-7fdc-4ace-80b8-2a22347dfac6"),
            AuthorId = UserSeeds.TestUser.Id,
            CategoryId = MissionCategorySeeds.Milsim.Id,
            ModpackTypeId = ModpackTypeSeeds.Moderna.Id
        };

        public static void Seed(ApplicationDbContext dbContext)
        {
            dbContext.Missions.AddRange(Mission1);
        }
    }
}
