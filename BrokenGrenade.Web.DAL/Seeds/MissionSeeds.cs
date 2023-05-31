using System.Globalization;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.DAL.Seeds;

public static class MissionSeeds
{
    public static readonly MissionEntity Mission1 = new("Operace Divoký bažant",
        DateTime.Parse("01/09/2023 19:00", CultureInfo.InvariantCulture))
    {
        Id = new Guid("7D854C14-A877-499A-9AA7-AB285A57769F"),

        CreatorId = UserSeeds.Administrator.Id,
        MissionTypeId = MissionTypeSeeds.Serious.Id,
        ModpackTypeId = ModpackTypeSeeds.Moderna.Id,
        SlottingSheetUrl = "https://google.com/"
    };

    public static readonly MissionEntity Mission2 = new("Den D",
        DateTime.Parse("01/10/2023 19:00", CultureInfo.InvariantCulture))
    {
        Id = new Guid("38AF7809-6340-4845-B737-0719B3894453"),
        
        SlottingSheetUrl = "https://google.com",
        ModpackUrl = "https://google.com",

        CreatorId = UserSeeds.Borek.Id,
        MissionTypeId = MissionTypeSeeds.Serious.Id,
        ModpackTypeId = ModpackTypeSeeds.Vietnam.Id
    };

    public static async Task SeedAsync(BrokenGrenadeDbContext dbContext)
    {
        await dbContext.MissionEntities.AddRangeAsync(Mission1, Mission2);
    }
}