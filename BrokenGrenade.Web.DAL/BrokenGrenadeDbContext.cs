﻿using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.DAL;

public class BrokenGrenadeDbContext : CustomApiAuthorizationDbContext<UserEntity, RoleEntity>
{
    public BrokenGrenadeDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<MissionEntity> MissionEntities => Set<MissionEntity>();
    public DbSet<MissionTypeEntity> MissionTypeEntities => Set<MissionTypeEntity>();
    public DbSet<ModpackTypeEntity> ModpackTypeEntities => Set<ModpackTypeEntity>();
    public DbSet<ApplicationEntity> ApplicationEntities => Set<ApplicationEntity>();
    public DbSet<ArticleEntity> ArticleEntities => Set<ArticleEntity>();
    public DbSet<PunishmentEntity> PunishmentEntities => Set<PunishmentEntity>();
    public DbSet<TrainingEntity> TrainingEntities => Set<TrainingEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<MissionTypeEntity>()
            .HasMany(i => i.Missions)
            .WithOne(i => i.MissionType)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<ModpackTypeEntity>()
            .HasMany(i => i.Missions)
            .WithOne(i => i.ModpackType)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<UserEntity>(entity =>
        {
            entity.HasMany(i => i.MissionsCreated)
                .WithOne(i => i.Creator)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(i => i.TrainingsCreated)
                .WithOne(i => i.Creator)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(i => i.ApplicationsEdited)
                .WithOne(i => i.EditedBy)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(i => i.TrainingsParticipating)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasMany(i => i.PunishmentsReceived)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.ClientCascade);

            entity.HasMany(i => i.PunishmentsIssued)
                .WithOne(i => i.IssuedBy)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(i => i.ArticlesCreated)
                .WithOne(i => i.Creator)
                .OnDelete(DeleteBehavior.SetNull);
            
            entity.HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        builder.Entity<ApplicationEntity>()
            .HasOne(i => i.User)
            .WithOne(i => i.Application)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        builder.Entity<TrainingEntity>()
            .HasMany(i => i.Participants)
            .WithOne(i => i.Training)
            .OnDelete(DeleteBehavior.Cascade);
    }
}