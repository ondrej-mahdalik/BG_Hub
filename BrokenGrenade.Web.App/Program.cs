using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BrokenGrenade.Common.Permissions;
using BrokenGrenade.Web.App.Extensions;
using BrokenGrenade.Web.App.Services;
using BrokenGrenade.Web.BL.Installers;
using BrokenGrenade.Web.DAL;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.Seeds;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    serviceCollection.AddControllersWithViews().AddNewtonsoftJson();
    serviceCollection.AddRazorPages();

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
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    serviceCollection.AddDbContextFactory<BrokenGrenadeDbContext>(options =>
        options.UseSqlServer(connectionString));

    serviceCollection.AddInstaller<WebBLInstaller>();

    serviceCollection.AddTransient<IEmailSender, EmailSender>();
    serviceCollection.Configure<AuthMessageSenderOptions>(
        configuration.GetSection(AuthMessageSenderOptions.SendGrid));
}

void ConfigureAuthentication(IServiceCollection serviceCollection, IConfiguration configuration)
{
    serviceCollection.AddDefaultIdentity<UserEntity>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<RoleEntity>()
        .AddEntityFrameworkStores<BrokenGrenadeDbContext>();

    serviceCollection.AddServerSideBlazor();
    serviceCollection.AddIdentityServer()
        .AddApiAuthorization<UserEntity, BrokenGrenadeDbContext>(options =>
        {
            options.IdentityResources["openid"].UserClaims.Add("role");
            options.ApiResources.Single().UserClaims.Add("role");
            options.IdentityResources["openid"].UserClaims.Add("permission");
            options.ApiResources.Single().UserClaims.Add("permission");
        });

    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");

    serviceCollection.AddAuthentication()
        .AddIdentityServerJwt();

    serviceCollection.AddAuthorization(options =>
    {
        options.AddPolicy(PolicyTypes.CreateMissions,
            policy => policy.RequireClaim("permission", PermissionTypes.CreateMissions));
        options.AddPolicy(PolicyTypes.ManageMissions,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageMissions));
        options.AddPolicy(PolicyTypes.ManageUsers,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageUsers));
        options.AddPolicy(PolicyTypes.ManageRoles,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageRoles));
        options.AddPolicy(PolicyTypes.ManageMissionTypes,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageMissionTypes));
        options.AddPolicy(PolicyTypes.ManageModpackTypes,
            policy => policy.RequireClaim("permission", PermissionTypes.ManageModpackTypes));
    });

    serviceCollection.Configure<IdentityOptions>(options =>
    {
        options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    });

    serviceCollection.Configure<JwtBearerOptions>(IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
        options => { options.Authority = configuration["IdentityServer:Authority"]; });

    serviceCollection.Configure<SecurityStampValidatorOptions>(options =>
    {
        options.ValidationInterval = TimeSpan.FromMinutes(5);
    });
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
    application.UseIdentityServer();
    application.UseAuthorization();
    application.MapRazorPages();
    application.MapControllers();
    application.MapFallbackToFile("/_Host");
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

        await RoleSeeds.Seed(roleManager);
        await UserSeeds.Seed(userManager);
        // TODO Seed other data
    }
    else
    {
        await dbx.Database.MigrateAsync();
        
        // Seed default role and admin account if missing
        if (!await roleManager.Roles.AnyAsync() && !await userManager.Users.AnyAsync())
        {
            await RoleSeeds.Seed(roleManager, true);
            await UserSeeds.Seed(userManager, true);
        }
    }
}

// Make the implicit Program class public so test projects can access it
public partial class Program
{
}