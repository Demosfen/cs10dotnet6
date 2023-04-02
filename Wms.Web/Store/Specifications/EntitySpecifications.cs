using Wms.Web.Store.Interfaces;

namespace Wms.Web.Store.Specifications;

public static class EntitySpecifications
{
    public static IQueryable<T> ById<T>(this IQueryable<T> source, Guid id)
        where T : IEntityWithId
        => source.Where(x => x.Id == id);

    public static IQueryable<T> NotDeleted<T>(this IQueryable<T> source)
        where T : ISoftDeletable
        => source.Where(x => !x.IsDeleted);
}