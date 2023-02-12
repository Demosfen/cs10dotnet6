using WMS.WarehouseDbContext.Interfaces;

namespace WMS.WarehouseDbContext.Specifications;

public static class EntitySpecifications
{
    public static IQueryable<T> ById<T>(this IQueryable<T> source, Guid id)
        where T : IEntityWithId
        => source.Where(x => x.Id == id);
}