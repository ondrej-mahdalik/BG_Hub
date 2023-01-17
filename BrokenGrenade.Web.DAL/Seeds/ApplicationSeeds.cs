using System.Globalization;
using BrokenGrenade.Common.Enums;
using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.DAL.Seeds;

public static class ApplicationSeeds
{
    public static readonly ApplicationEntity Application1 = new("Bořek",
        "borek.stavitel@gmail.com",
        "Borek#1234",
        "https://steamcommunity.com/id/AlmightyOndrej/",
        22,
        4000,
        "Ahoj, já jsem Bořek a chci se přidat. Děkuji.",
        "Nemám",
        "Zaujali jste mě, tak jsem se rozhodl přidat.",
        "Aktivitu na misích",
        "Sociální sítě")
    {
        Id = new Guid("2C788A46-F294-4097-A440-0E437E3439A8"),
        CreatedAt = DateTime.Parse("01/17/2023 12:30:00", CultureInfo.InvariantCulture),
        EditedById = UserSeeds.Administrator.Id,
        UserId = UserSeeds.Borek.Id,
        Status = ApplicationStatus.Accepted
    };

    public static async Task SeedAsync(BrokenGrenadeDbContext dbContext)
    {
        await dbContext.ApplicationEntities.AddRangeAsync(Application1);
    }
}