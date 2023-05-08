using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wms.Web.Store.Interfaces;

namespace Wms.Web.Store.Specifications;

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