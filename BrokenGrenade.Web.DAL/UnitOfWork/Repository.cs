using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using BrokenGrenade.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BrokenGrenade.Web.DAL.UnitOfWork;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly IModel _model;

    public Repository(DbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
        _model = dbContext.Model;
    }

    public IQueryable<TEntity> Get()
    {
        return _dbSet.IncludeFirstLevelNavigationProperties(_model);
    }

    public async Task<TEntity> InsertOrUpdateAsync<TModel>(
        TModel model,
        IMapper mapper,
        CancellationToken cancellationToken = default) where TModel : class
    {
        await _dbSet.PreLoadChangeTracker(mapper.Map<TEntity>(model).Id, _model, cancellationToken);

        return await _dbSet.Persist(mapper).InsertOrUpdateAsync(model, cancellationToken);
    }

    public void Delete(Guid entityId)
    {
        _dbSet.Remove(_dbSet.Single(i => i.Id == entityId));
    }

    public void DeleteRange(IEnumerable<Guid> entityIds)
    {
        _dbSet.RemoveRange(_dbSet.Where(i => entityIds.Contains(i.Id)));
    }
}