using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.DAL.Seeds;

public static class MissionTypeSeeds
{
    public static readonly MissionTypeEntity Fun = new("Fun")
    {
        Id = new Guid("2AB2107F-5EB3-4B80-9CBC-400325C35183")
    };

    public static readonly MissionTypeEntity Serious = new("Serious",
        "Jedná se o serious misi, nezapomeň si přečíst pravidla!")
    {
        Id = new Guid("26DF9CD7-ADBB-4E00-B100-C8B1D7FB99FC")
    };

    public static async Task SeedAsync(BrokenGrenadeDbContext dbContext)
    {
        await dbContext.MissionTypeEntities.AddRangeAsync(Fun, Serious);
    }
}