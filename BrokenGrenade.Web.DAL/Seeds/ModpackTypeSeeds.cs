using BrokenGrenade.Web.DAL.Entities;

namespace BrokenGrenade.Web.DAL.Seeds;

public static class ModpackTypeSeeds
{
    public static readonly ModpackTypeEntity Moderna = new("Moderna")
    {
        Id = new Guid("57D9DC9B-42D0-4FC8-B451-5436E225BC43")
    };

    public static readonly ModpackTypeEntity Vietnam = new("Vietnam")
    {
        Id = new Guid("1D6AF088-41B2-4BAE-A3BC-BEA84B2658AD")
    };

    public static async Task SeedAsync(BrokenGrenadeDbContext dbContext)
    {
        await dbContext.ModpackTypeEntities.AddRangeAsync(Moderna, Vietnam);
    }
}