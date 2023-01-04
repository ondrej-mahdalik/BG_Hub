using BrokenGrenade.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BrokenGrenade.Web.DAL.UnitOfWork;

public static class QueryableExtensions
{
    public static async Task PreLoadChangeTracker<TEntity>(this IQueryable<TEntity> dbSet, Guid entityId, IModel model,
        CancellationToken cancellationToken) where TEntity : class, IEntity
    {
        await dbSet
            .IncludeFirstLevelNavigationProperties(model)
            .Where(e => e.Id == entityId)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public static IQueryable<TEntity> IncludeFirstLevelNavigationProperties<TEntity>(this IQueryable<TEntity> query,
        IModel model) where TEntity : class
    {
        var navigationProperties = model.FindEntityType(typeof(TEntity))?.GetNavigations();
        return navigationProperties == null
            ? query
            : navigationProperties.Aggregate(query,
                (current, navigationProperty) => current.Include(navigationProperty.Name));
    }
}