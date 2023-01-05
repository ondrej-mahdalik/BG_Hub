using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.Common.Facades;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.BL.Facades;

public class CRUDFacade<TEntity, TModel> : IAppFacade
    where TEntity : class, IEntity
    where TModel : class, IModel
{
    protected readonly IMapper Mapper;
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory;

    public CRUDFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
    {
        UnitOfWorkFactory = unitOfWorkFactory;
        Mapper = mapper;
    }

    public virtual async Task DeleteAsync(TModel model)
    {
        await DeleteAsync(model.Id);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        uow.GetRepository<TEntity>().Delete(id);
        await uow.CommitAsync().ConfigureAwait(false);
    }

    public virtual async Task<TModel?> GetAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TEntity>()
            .Get()
            .Where(e => e.Id == id);

        return await Mapper.ProjectTo<TModel>(query).SingleOrDefaultAsync().ConfigureAwait(false);
    }

    public virtual async Task<List<TModel>> GetAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TEntity>()
            .Get();

        return await Mapper.ProjectTo<TModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public virtual async Task<TModel> SaveAsync(TModel model)
    {
        await using var uow = UnitOfWorkFactory.Create();

        var entity = await uow
            .GetRepository<TEntity>()
            .InsertOrUpdateAsync(model, Mapper)
            .ConfigureAwait(false);
        await uow.CommitAsync();

        return (await GetAsync(entity.Id).ConfigureAwait(false))!;
    }
}