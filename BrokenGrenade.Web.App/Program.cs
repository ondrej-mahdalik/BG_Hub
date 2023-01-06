using BrokenGrenade.Common.Permissions;
using BrokenGrenade.Web.App.Areas.Identity;
using BrokenGrenade.Web.App.Extensions;
using BrokenGrenade.Web.App.Services;
using BrokenGrenade.Web.BL.Installers;
using BrokenGrenade.Web.DAL;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.Seeds;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

builder.Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile("appsettings.Development.json", true, true)
    .AddJsonFile("appsettings.Secrets.json", true, true)
    .AddEnvironmentVariables();

ConfigureControllers(builder.Services);
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAuthentication(builder.Services, builder.Configuration);

var app = builder.Build();

UseDevelopmentSettings(app);
UseRoutingAndSecurityFeatures(app);
await SetupDatabaseAsync(app);

app.Run();


void ConfigureControllers(IServiceCollection serviceCollection)
{
    serviceCollection.AddRazorPages();
    serviceCollection.AddServerSideBlazor();
    serviceCollection.AddControllers()
        .AddNewtonsoftJson();

    serviceCollection.AddFluentValidationAutoValidation();
    serviceCollection.AddValidatorsFromAssemblyContaining<WebBLInstaller>();

    serviceCollection.AddCors(options =>
    {
        // TODO Change in production
        options.AddDefaultPolicy(policy =>
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
}

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
    var dbType = configuration.GetValue<string>("DALOptions:DBType");
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    switch (dbType)
    {
        case "SQLServer":
            serviceCollection.AddDbContextFactory<BrokenGrenadeDbContext>(options =>
                options.UseSqlServer(connectionString));
            break;
            
        case "SQLite":
            serviceCollection.AddDbContextFactory<BrokenGrenadeDbContext>(options =>
                options.UseSqlite(connectionString));
            break;
    }

    serviceCollection.AddInstaller<WebBLInstaller>();

    serviceCollection.AddTransient<IEmailSender, EmailSender>();
    serviceCollection.Configure<AuthMessageSenderOptions>(
        configuration.GetSection(AuthMessageSenderOptions.SendGrid));
}

void ConfigureAuthentication(IServiceCollection serviceCollection, IConfiguration configuration)
{
    serviceCollection.AddDefaultIdentity<UserEntity>(options => options.SignIn.RequireConfirmedEmail = true)
        .AddRoles<RoleEntity>()
        .AddEntityFrameworkStores<BrokenGrenadeDbContext>();
    // serviceCollection.AddIdentityServer()
    //     .AddApiAuthorization<UserEntity, BrokenGrenadeDbContext>(options =>
    //     {
    //         options.IdentityResources["openid"].UserClaims.Add("role");
    //         options.ApiResources.Single().UserClaims.Add("role");
    //         options.IdentityResources["openid"].UserClaims.Add("permission");
    //         options.ApiResources.Single().UserClaims.Add("permission");
    //     });
    serviceCollection.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<UserEntity>>();

    serviceCollection.AddAuthentication();

    serviceCollection.AddAuthorization(options =>
    {
        options.AddPolicy(PolicyTypes.CreateMissions,
            policy => policy.RequireClaim("permission", PermissionTypes.CreateMissions));
        options.AddPolicy(PolicyTypes.CreateTrainings,
            policy => policy.RequireClaim("permission", PermissionTypes.CreateTrainings));
        options.AddPolicy(PolicyTypes.ManageMissions,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageMissions));
        options.AddPolicy(PolicyTypes.ManageTrainings,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageTrainings));
        options.AddPolicy(PolicyTypes.ManageUsers,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageUsers));
        options.AddPolicy(PolicyTypes.ManageRoles,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageRoles));
        options.AddPolicy(PolicyTypes.ManageMissionTypes,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageMissionTypes));
        options.AddPolicy(PolicyTypes.ManageModpackTypes,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageModpackTypes));
        options.AddPolicy(PolicyTypes.ManageApplications,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageApplications));
        
        options.AddPolicy(PolicyTypes.IsPlatoonLead,
            policy => policy.RequireAssertion(context =>
                context.User.HasClaim(c => c is 
                { Type: "permission", Value: PermissionTypes.ManageUsers
                    or PermissionTypes.ManageRoles
                    or PermissionTypes.ManageMissionTypes
                    or PermissionTypes.ManageModpackTypes
                    or PermissionTypes.ManageApplications
                    or PermissionTypes.ManageMissions
                    or PermissionTypes.ManageTrainings
                })));
    });

    // serviceCollection.Configure<IdentityOptions>(options =>
    // {
    //     options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    // });

    // serviceCollection.Configure<JwtBearerOptions>(IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
    //     options => { options.Authority = configuration["IdentityServer:Authority"]; });

    // serviceCollection.Configure<SecurityStampValidatorOptions>(options =>
    // {
    //     options.ValidationInterval = TimeSpan.FromMinutes(5);
    // });
}

void UseDevelopmentSettings(WebApplication application)
{
    // Configure the HTTP request pipeline.
    if (application.Environment.IsDevelopment())
    {
        application.UseMigrationsEndPoint();
    }
    else
    {
        application.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        application.UseHsts();
    }
}

void UseRoutingAndSecurityFeatures(WebApplication application)
{
    application.UseHttpsRedirection();
    application.UseStaticFiles();
    application.UseRouting();
    application.UseRequestLocalization("cs-CZ");
    // application.UseIdentityServer();
    application.UseAuthentication();
    application.UseAuthorization();
    application.MapControllers();
    application.MapBlazorHub();
    application.MapFallbackToPage("/_Host");
}

async Task SetupDatabaseAsync(WebApplication application)
{
    var scope = application.Services.CreateAsyncScope();
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BrokenGrenadeDbContext>>();
    var skipMigrationAndSeedDemoData = application.Configuration.GetSection("DALOptions")
        .GetValue<bool>("SkipMigrationAndSeedDemoData");

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

    await using var dbx = dbContextFactory.CreateDbContext();
    if (skipMigrationAndSeedDemoData)
    {
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();

        await RoleSeeds.SeedAsync(roleManager);
        await UserSeeds.SeedAsync(userManager);
        await MissionTypeSeeds.SeedAsync(dbx);
        await ModpackTypeSeeds.SeedAsync(dbx);
        await MissionSeeds.SeedAsync(dbx);
        await dbx.SaveChangesAsync();
    }
    else
    {
        await dbx.Database.MigrateAsync();
        
        // Seed default role and admin account if missing
        if (!await roleManager.Roles.AnyAsync() && !await userManager.Users.AnyAsync())
        {
            await RoleSeeds.SeedAsync(roleManager, true);
            await UserSeeds.SeedAsync(userManager, true);
        }
    }
}

// Make the implicit Program class public so test projects can access it
public partial class Program
{
}