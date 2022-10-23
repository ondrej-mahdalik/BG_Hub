using BrokenGrenade.Data;

namespace BrokenGrenade.Seeds;

public static class MissionCategorySeeds
{
    public static readonly MissionCategory Serious = new("Serious", "")
    {
        Id = Guid.Parse("B3E9F5BB-A2C5-4ECF-A545-D8076E5AFFD8")
    };

    public static readonly MissionCategory Milsim = new("Milsim", "Jedná se o milsim misi. Nechovej se, jak hovado")
    {
        Id = Guid.Parse("78324511-859A-496B-AC0C-2054A26E64A7")
    };

    public static void Seed(ApplicationDbContext dbContext)
    {
        dbContext.Categories.AddRange(Serious, Milsim);
    }
}