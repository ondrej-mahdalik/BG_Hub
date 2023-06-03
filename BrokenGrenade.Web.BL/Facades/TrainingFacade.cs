using AutoMapper;
using BrokenGrenade.Common.Enums;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Common.Models.Filters;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.BL.Facades;

public class TrainingFacade : CRUDFacade<TrainingEntity, TrainingModel>
{
    private DiscordWebhookSender _discordWebhookSender;
    
    public TrainingFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, DiscordWebhookSender discordWebhookSender) : base(unitOfWorkFactory, mapper)
    {
        _discordWebhookSender = discordWebhookSender;
    }

    public async Task<List<TrainingModel>> GetUpcomingAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<TrainingEntity>()
            .Get()
            .Include(i => i.Creator)
            .Where(x => x.Date.Date > DateTime.Today);

        return await Mapper.ProjectTo<TrainingModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<TrainingModel>> GetAsync(TrainingFilterModel filter)
    {
        var query = GetFiltered(filter);
        
        return await Mapper.ProjectTo<TrainingModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<TrainingModel>> GetPaginatedAsync(TrainingFilterModel filter, int page)
    {
        var query = GetFiltered(filter);
        query = query
            .OrderByDescending(x => x.Date)
            .ThenBy(x => x.Id)
            .Skip(page * 10)
            .Take(10);

        return await Mapper.ProjectTo<TrainingModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<int> GetCountAsync(TrainingFilterModel filter)
    {
        var query = GetFiltered(filter);
        return await query.CountAsync().ConfigureAwait(false);
    }

    private IQueryable<TrainingEntity> GetFiltered(TrainingFilterModel filter)
    {
        var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<TrainingEntity>()
            .Get()
            .Include(x => x.Creator)
            .Include(x => x.Participants)
            .Where(x => true);

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        
        if(!string.IsNullOrWhiteSpace(filter.Creator))
            query = query.Where(x => x.Creator != null && x.Creator.Nickname.ToLower().Contains(filter.Creator.ToLower()));
        
        if(filter.Date.HasValue)
            query = query.Where(x => x.Date.Date == filter.Date.Value.Date);

        return query;
    }

    public async Task<TrainingModel> SaveAsync(TrainingModel model, TrainingMentionType mentionType)
    {
        var exists = await GetAsync(model.Id) is not null;
        if (!exists)
        {
            model = await base.SaveAsync(model);
            model.DiscordMessageId = await _discordWebhookSender.SendTrainingAsync(model, mentionType);
        }

        return await base.SaveAsync(model);
    }
}