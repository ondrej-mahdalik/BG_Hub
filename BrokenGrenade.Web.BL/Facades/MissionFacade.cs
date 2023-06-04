using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Common.Models.Filters;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.BL.Facades;

public class MissionFacade : CRUDFacade<MissionEntity, MissionModel>
{
    private DiscordWebhookSender _discordWebhookSender;
    
    public MissionFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, DiscordWebhookSender discordWebhookSender) : base(unitOfWorkFactory, mapper)
    {
        _discordWebhookSender = discordWebhookSender;
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
        var query = GetFiltered(filter);
        return await Mapper.ProjectTo<MissionModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<int> GetCountAsync(MissionFilterModel filterModel)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = GetFiltered(filterModel);
        return await query.CountAsync().ConfigureAwait(false);
    }

    public async Task<List<MissionModel>> GetPaginatedAsync(MissionFilterModel filter, int page)
    {
        var query = GetFiltered(filter);
        query = query
            .OrderByDescending(x => x.MissionStartDate)
            .ThenBy(x => x.Id)
            .Skip(page * 10)
            .Take(10);
        
        return await Mapper.ProjectTo<MissionModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<MissionModel>> GetUpcomingAsync(int days, MissionFilterModel? filter = null)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = GetFiltered(filter);
        query = query.Where(x => x.MissionStartDate.Date <= DateTime.Today.AddDays(days));
        
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
    
    public async Task<bool> IsMissionOn(DateTime date, Guid? missionToIgnore = null)
    {
        await using var uow = UnitOfWorkFactory.Create();
        return await uow.GetRepository<MissionEntity>().Get().AnyAsync(x => x.MissionStartDate.Date == date.Date && x.Id != missionToIgnore).ConfigureAwait(false);
    }

    public override async Task<MissionModel> SaveAsync(MissionModel model)
    {
        var exists = await GetAsync(model.Id) is not null;
        if (!exists)
        {
            model = await base.SaveAsync(model);
            model.DiscordMessageId = await _discordWebhookSender.SendMissionAsync(model);
        }

        return await base.SaveAsync(model);
    }

    private IQueryable<MissionEntity> GetFiltered(MissionFilterModel? filter)
    {
        var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<MissionEntity>()
            .Get()
            .Include(x => x.ModpackType)
            .Include(x => x.MissionType)
            .Include(x => x.Creator)
            .Where(x => true);

        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        
        if(!string.IsNullOrWhiteSpace(filter.Creator))
            query = query.Where(x => x.Creator != null && x.Creator.Nickname.ToLower().Contains(filter.Creator.ToLower()));

        if (filter.CreatorId is not null)
            query = query.Where(x => x.CreatorId == filter.CreatorId);
        
        if(filter.ModpackType is not null)
            query = query.Where(x => x.ModpackTypeId == filter.ModpackType);
        
        if(filter.MissionType is not null)
            query = query.Where(x => x.MissionTypeId == filter.MissionType);
        
        if(filter.Date.HasValue)
            query = query.Where(x => x.MissionStartDate.Date == filter.Date.Value.Date);
        
        return query;
    }
}