using AutoMapper;
using BrokenGrenade.Common.Enums;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Common.Models.Filters;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.BL.Facades;

public class ApplicationFacade : CRUDFacade<ApplicationEntity, ApplicationModel>
{
    private DiscordWebhookSender _discordWebhookSender;
    
    public ApplicationFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, DiscordWebhookSender discordWebhookSender) : base(unitOfWorkFactory, mapper)
    {
        _discordWebhookSender = discordWebhookSender;
    }

    public async Task<List<ApplicationModel>> GetPaginatedAsync(ApplicationFilterModel filter, int page)
    {
        var query = GetFiltered(filter);
        query = query
            .Include(x => x.EditedBy)
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Id)
            .Skip(page * 10)
            .Take(10);

        var entities = await query.ToListAsync();

        return await Mapper.ProjectTo<ApplicationModel>(query).ToListAsync().ConfigureAwait(false);

    }

    public async Task<int> GetUnhanledApplicationsCountAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        return await uow.GetRepository<ApplicationEntity>()
            .Get()
            .CountAsync(x => x.Status == ApplicationStatus.AwaitingDecision);
    }

    public async Task<ApplicationModel?> GetByUserAsync(Guid userId)
    {
            await using var uow = UnitOfWorkFactory.Create();
        var entity = await uow.GetRepository<ApplicationEntity>()
            .Get()
            .FirstOrDefaultAsync(x => x.UserId == userId);

        return entity == null ? null : Mapper.Map<ApplicationModel>(entity);
    }

    public async Task<int> GetCountAsync(ApplicationFilterModel filter)
    {
        var query = GetFiltered(filter);
        return await query.CountAsync();
    }

    public override async Task<ApplicationModel> SaveAsync(ApplicationModel model)
    {
        var exists = await GetAsync(model.Id) is not null;
        if (!exists)
        {
            model = await base.SaveAsync(model);
            await _discordWebhookSender.SendApplicationAsync(model);
        }

        return await base.SaveAsync(model);
    }

    private IQueryable<ApplicationEntity> GetFiltered(ApplicationFilterModel? filter)
    {
        var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<ApplicationEntity>().Get();

        if (filter is null)
            return query;

        if (filter.Status.HasValue)
            query = query.Where(x => x.Status == filter.Status);
        
        if (filter.UserId.HasValue)
            query = query.Where(x => x.UserId == filter.UserId);
        
        if (filter.Discord is not null)
            query = query.Where(x => x.Discord.ToLower().Contains(filter.Discord.ToLower()));
        
        if (filter.Email is not null)
            query = query.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
        
        if (filter.Nickname is not null)
            query = query.Where(x => x.Nickname.ToLower().Contains(filter.Nickname.ToLower()));
        
        if (filter.Age.HasValue)
            query = query.Where(x => x.Age == filter.Age);
        
        if (filter.Steam is not null)
            query = query.Where(x => x.SteamUrl.ToLower().Contains(filter.Steam.ToLower()));
        
        if (filter.CreatedAt.HasValue)
            query = query.Where(x => x.CreatedAt.Date == filter.CreatedAt.Value.Date);
        
        if (filter.EditedById.HasValue)
            query = query.Where(x => x.EditedById == filter.EditedById);

        return query;
    }
}