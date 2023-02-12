namespace WMS.WarehouseDbContext.Interfaces;

public interface IEntityWithId
{
    Guid Id { get; init; }
}