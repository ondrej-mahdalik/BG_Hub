using System.Globalization;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.DAL.Seeds;

public static class TrainingSeeds
{
    public static readonly TrainingEntity Training1 = new("Artillery výcvik",
        DateTime.Parse("01/18/2023 15:00", CultureInfo.InvariantCulture),
        "Výcvik pro všechny, kteří se chtějí naučit využívat dělostřelecké zbraně.")
    {
        Id = new Guid("4A5FE459-B269-4DEB-9C81-C4E74B0F8316"),
        CreatorId = UserSeeds.Borek.Id
    };

    public static async Task SeedAsync(BrokenGrenadeDbContext dbContext)
    {
        await dbContext.TrainingEntities.AddRangeAsync(Training1);
    }

}
