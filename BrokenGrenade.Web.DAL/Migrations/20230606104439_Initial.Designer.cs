﻿// <auto-generated />
using System;
using BrokenGrenade.Web.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BrokenGrenade.Web.DAL.Migrations
{
    [DbContext(typeof(BrokenGrenadeDbContext))]
    [Migration("20230606104439_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.ApplicationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("AcceptedGdpr")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AgreedToRules")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discord")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EditedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HowDidYouFindUs")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("MeetsRequirements")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PreviousExperience")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReasonToJoin")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StaffComment")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SteamUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("WhatCanYouOffer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EditedById");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ApplicationEntities");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.ArticleCategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("ArticleCategoryEntities");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.ArticleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ArticleEntities");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.MissionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("TEXT");

                    b.Property<ulong?>("DiscordMessageId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("MissionStartDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("MissionTypeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModpackTypeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModpackUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SlottingSheetUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("MissionTypeId");

                    b.HasIndex("ModpackTypeId");

                    b.ToTable("MissionEntities");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.MissionTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MissionTypeEntities");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.ModpackTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ModpackTypeEntities");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.PunishmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ExpiresOn")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("IssuedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("IssuedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Punishment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IssuedById");

                    b.HasIndex("UserId");

                    b.ToTable("PunishmentEntities");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ColorHex")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.TrainingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<ulong?>("DiscordMessageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("TrainingEntities");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.UserIsParticipatingTrainingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TrainingId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TrainingId");

                    b.HasIndex("UserId");

                    b.ToTable("UserIsParticipatingTrainingEntity");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Use")
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Use");

                    b.ToTable("Keys", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.HasIndex("ConsumedTime");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.ApplicationEntity", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", "EditedBy")
                        .WithMany("ApplicationsEdited")
                        .HasForeignKey("EditedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", "User")
                        .WithOne("Application")
                        .HasForeignKey("BrokenGrenade.Web.DAL.Entities.ApplicationEntity", "UserId");

                    b.Navigation("EditedBy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.ArticleCategoryEntity", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.ArticleCategoryEntity", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.ArticleEntity", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.ArticleCategoryEntity", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.MissionEntity", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", "Creator")
                        .WithMany("MissionsCreated")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BrokenGrenade.Web.DAL.Entities.MissionTypeEntity", "MissionType")
                        .WithMany("Missions")
                        .HasForeignKey("MissionTypeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BrokenGrenade.Web.DAL.Entities.ModpackTypeEntity", "ModpackType")
                        .WithMany("Missions")
                        .HasForeignKey("ModpackTypeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Creator");

                    b.Navigation("MissionType");

                    b.Navigation("ModpackType");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.PunishmentEntity", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", "IssuedBy")
                        .WithMany("PunishmentsIssued")
                        .HasForeignKey("IssuedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", "User")
                        .WithMany("PunishmentsReceived")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("IssuedBy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.TrainingEntity", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", "Creator")
                        .WithMany("TrainingsCreated")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.UserIsParticipatingTrainingEntity", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.TrainingEntity", "Training")
                        .WithMany("Participants")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", "User")
                        .WithMany("TrainingsParticipating")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Training");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("BrokenGrenade.Web.DAL.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.ArticleCategoryEntity", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Children");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.MissionTypeEntity", b =>
                {
                    b.Navigation("Missions");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.ModpackTypeEntity", b =>
                {
                    b.Navigation("Missions");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.TrainingEntity", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("BrokenGrenade.Web.DAL.Entities.UserEntity", b =>
                {
                    b.Navigation("Application");

                    b.Navigation("ApplicationsEdited");

                    b.Navigation("MissionsCreated");

                    b.Navigation("PunishmentsIssued");

                    b.Navigation("PunishmentsReceived");

                    b.Navigation("TrainingsCreated");

                    b.Navigation("TrainingsParticipating");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
