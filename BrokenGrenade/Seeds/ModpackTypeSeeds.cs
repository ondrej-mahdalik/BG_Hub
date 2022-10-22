using BrokenGrenade.Data;

namespace BrokenGrenade.Seeds;

public class ModpackTypeSeeds
{
    public static readonly ModpackType Moderna = new("Moderna")
    {
        Id = Guid.Parse("8C3FEC1D-7838-4A8A-9BEF-3375F5A93710")
    };
    public static readonly ModpackType WW2 = new("WW2")
    {
        Id = Guid.Parse("E1B0E9A6-2298-4B15-8919-9B88095DAD88")
    };

    public static void Seed(ApplicationDbContext dbContext)
    {
        dbContext.AddRange(Moderna, WW2);
    }
}