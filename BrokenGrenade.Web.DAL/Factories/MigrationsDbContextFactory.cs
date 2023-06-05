using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace BrokenGrenade.Web.DAL.Factories;

public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<BrokenGrenadeDbContext>
{

    public BrokenGrenadeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BrokenGrenadeDbContext>();
        optionsBuilder.UseSqlServer("Data Source=localhost,1433;User ID=sa;Password=Pass123$;Trust Server Certificate=True;Initial Catalog=BrokenGrenade.Main");

        return new BrokenGrenadeDbContext(optionsBuilder.Options);
    }
}