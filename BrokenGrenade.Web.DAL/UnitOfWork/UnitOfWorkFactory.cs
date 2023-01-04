using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<BrokenGrenadeDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<BrokenGrenadeDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public IUnitOfWork Create()
    {
        return new UnitOfWork(_dbContextFactory.CreateDbContext());
    }
}
