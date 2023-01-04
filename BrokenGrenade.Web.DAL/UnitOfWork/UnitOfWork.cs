using BrokenGrenade.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.DAL.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
    {
        return new Repository<TEntity>(_dbContext);
    }

    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}
