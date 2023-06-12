using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Common.Models.Filters;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.BL.Facades;

public class ArticleFacade : CRUDFacade<ArticleEntity, ArticleModel>
{
    public ArticleFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }

    public async Task<List<ArticleModel>> GetPaginatedAsync(ArticleFilterModel filter, int page)
    {
        var query = GetFiltered(filter)
            .OrderByDescending(x => x.Id)
            .Skip(page * 10)
            .Take(10);
        
        return await Mapper.ProjectTo<ArticleModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<int> GetCountAsync(ArticleFilterModel filter)
    {
        return await GetFiltered(filter).CountAsync().ConfigureAwait(false);
    }
    
    private IQueryable<ArticleEntity> GetFiltered(ArticleFilterModel? filter)
    {
        var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<ArticleEntity>()
            .Get()
            .AsQueryable();

        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

        if (filter.CreatorId is not null)
            query = query.Where(x => x.CreatorId == filter.CreatorId);

        return query;
    }
}