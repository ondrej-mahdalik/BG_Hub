﻿using AutoMapper;
using BrokenGrenade.Common.Enums;
using BrokenGrenade.Common.Models;
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
}