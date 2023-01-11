using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.DAL.Seeds;

public static class PunishmentSeeds
{
    public static readonly PunishmentEntity KarelPunishment = new("Karlos",
        "Vše",
        DateTime.Parse("01/11/2022 18:40"))
    {
        Id = new Guid("59DDB315-771A-43F2-9E9A-FE238C1692B5"),
        UserId = UserSeeds.Borek.Id,
        IssuedById = UserSeeds.Administrator.Id
    };

    public static async Task SeedAsync(BrokenGrenadeDbContext dbContext)
    {
        await dbContext.PunishmentEntities.AddAsync(KarelPunishment);
    }
}