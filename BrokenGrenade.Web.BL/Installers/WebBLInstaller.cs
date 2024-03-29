﻿using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BrokenGrenade.Web.BL.Facades;
using BrokenGrenade.Web.Common.Installers;
using BrokenGrenade.Web.DAL;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrokenGrenade.Web.BL.Installers;

public class WebBLInstaller : IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
        serviceCollection.AddSingleton<TrainingFacade>();
        serviceCollection.AddSingleton<UserIsParticipatingTrainingFacade>();
        serviceCollection.AddSingleton<MissionFacade>();
        serviceCollection.AddSingleton<MissionTypeFacade>();
        serviceCollection.AddSingleton<ModpackTypeFacade>();
        serviceCollection.AddSingleton<ApplicationFacade>();
        serviceCollection.AddSingleton<ArticleFacade>();
        serviceCollection.AddSingleton<PunishmentFacade>();
        serviceCollection.AddScoped<RoleFacade>();
        serviceCollection.AddScoped<UserFacade>();
        serviceCollection.AddSingleton<DiscordWebhookSender>();

        serviceCollection.AddAutoMapper((serviceProvider, cfg) =>
        {
            cfg.AddCollectionMappers();

            var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<BrokenGrenadeDbContext>>();
            using var dbContext = dbContextFactory.CreateDbContext();
            cfg.UseEntityFrameworkCoreModel(dbContext.Model);
        }, typeof(BusinessLogic).Assembly);
    }
}