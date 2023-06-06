using Wms.Web.Store.Entities.Interfaces;

namespace Wms.Web.Store.Entities.Specifications;

/// <summary>
/// Entities specifications
/// </summary>
public static class EntitySpecifications
{
    public static IQueryable<T> ById<T>(this IQueryable<T> source, Guid id)
        where T : IEntityWithId
        => source.Where(x => x.Id == id);

    public static IQueryable<T> NotDeleted<T>(this IQueryable<T> source)
        where T : IAuditableEntity
        => source.Where(x => x.DeletedAt == null);

    public static IQueryable<T> Deleted<T>(this IQueryable<T> source)
        where T : IAuditableEntity
        => source.Where(x => x.DeletedAt != null);
}