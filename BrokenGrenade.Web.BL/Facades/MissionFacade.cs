using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.BL.Facades;

public class MissionFacade : CRUDFacade<MissionEntity, MissionModel>
{
    public MissionFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }

    public override async Task<MissionModel?> GetAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<MissionEntity>()
            .Get()
            .Include(x => x.ModpackType)
            .Include(x => x.MissionType)
            .Include(x => x.Creator)
            .Where(e => e.Id == id);

        return await Mapper.ProjectTo<MissionModel>(query).SingleOrDefaultAsync().ConfigureAwait(false);
    }

    public override async Task<List<MissionModel>> GetAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<MissionEntity>()
            .Get()
            .Include(x => x.ModpackType)
            .Include(x => x.MissionType)
            .Include(x => x.Creator);

        return await Mapper.ProjectTo<MissionModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<MissionModel>> GetAsync(MissionFilterModel filter)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<MissionEntity>()
            .Get()
            .Include(x => x.ModpackType)
            .Include(x => x.MissionType)
            .Include(x => x.Creator)
            .Where(x => true);

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        
        if(!string.IsNullOrWhiteSpace(filter.Creator))
            query = query.Where(x => x.Creator != null && x.Creator.Nickname.Contains(filter.Creator));
        
        if(filter.ModpackType is not null)
            query = query.Where(x => x.ModpackTypeId == filter.ModpackType);
        
        if(filter.MissionType is not null)
            query = query.Where(x => x.MissionTypeId == filter.MissionType);
        
        if(filter.Date.HasValue)
            query = query.Where(x => x.MissionStartDate.Date == filter.Date.Value.Date);
        
        return await Mapper.ProjectTo<MissionModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<MissionModel>> GetUpcomingAsync(int days, MissionFilterModel? filter = null)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<MissionEntity>()
            .Get()
            .Include(x => x.ModpackType)
            .Include(x => x.MissionType)
            .Include(x => x.Creator)
            .Where(x => x.MissionStartDate.Date >= DateTime.Now.Date && x.MissionStartDate.Date <= DateTime.Now.AddDays(days).Date);

        if (filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));
        
            if(!string.IsNullOrWhiteSpace(filter.Creator))
                query = query.Where(x => x.Creator != null && x.Creator.Nickname.Contains(filter.Creator));
        
            if(filter.ModpackType is not null)
                query = query.Where(x => x.ModpackTypeId == filter.ModpackType);
        
            if(filter.MissionType is not null)
                query = query.Where(x => x.MissionTypeId == filter.MissionType);
        
            if(filter.Date.HasValue)
                query = query.Where(x => x.MissionStartDate.Date == filter.Date.Value.Date);
        }
        
        return await Mapper.ProjectTo<MissionModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<MissionModel>> GetByUserAsync(Guid userId)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<MissionEntity>()
            .Get()
            .Where(x => x.CreatorId == userId);

        return await Mapper.ProjectTo<MissionModel>(query).ToListAsync().ConfigureAwait(false);
    }
}