using BrokenGrenade.Areas.Identity;
using BrokenGrenade.Data;
using BrokenGrenade.Seeds;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanViewStaffMenu", policy => policy.RequireClaim("ViewStaffMenu", "true"));
    options.AddPolicy("CanCreateMissions", policy => policy.RequireClaim("CreateMissions", "true"));
    options.AddPolicy("CanManageMissions", policy => policy.RequireClaim("ManageMissions", "true"));
    options.AddPolicy("CanManageUsers", policy => policy.RequireClaim("ManageUsers", "true"));
});
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
builder.Services.AddScoped<MissionService>();
builder.Services.AddScoped<MissionCategoryService>();
builder.Services.AddScoped<ModpackTypeService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
await SetupDatabase(app);

async Task SetupDatabase(WebApplication application)
{
    var scope = application.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();

    await RoleSeeds.Seed(scope.ServiceProvider);
    await UserSeeds.Seed(scope.ServiceProvider);
    MissionSeeds.Seed(dbContext);
    ModpackTypeSeeds.Seed(dbContext);
    MissionCategorySeeds.Seed(dbContext);

    await dbContext.SaveChangesAsync();
}

app.Run();
