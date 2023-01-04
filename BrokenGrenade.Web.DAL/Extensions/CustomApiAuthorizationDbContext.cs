using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.DAL.Extensions;

public abstract class CustomApiAuthorizationDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, Guid>,
    IPersistedGrantDbContext
    where TUser : IdentityUser<Guid>
    where TRole : IdentityRole<Guid>
{
    /// <summary>
    ///     Initializes a new instance of <see cref="ApiAuthorizationDbContext{TUser}" />.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions" />.</param>
    public CustomApiAuthorizationDbContext(
        DbContextOptions options)
        : base(options)
    {
    }

    /// <summary>
    ///     Gets or sets the <see cref="DbSet{PersistedGrant}" />.
    /// </summary>
    public DbSet<PersistedGrant> PersistedGrants { get; set; }

    /// <summary>
    ///     Gets or sets the <see cref="DbSet{DeviceFlowCodes}" />.
    /// </summary>
    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

    /// <summary>
    ///     Gets or sets the <see cref="DbSet{Key}" />.
    /// </summary>
    public DbSet<Key> Keys { get; set; }

    Task<int> IPersistedGrantDbContext.SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var storeOptions = new OperationalStoreOptions();
        builder.ConfigurePersistedGrantContext(storeOptions);
    }
}