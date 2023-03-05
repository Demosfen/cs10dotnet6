namespace WMS.WarehouseDbContext.Interfaces;

public interface ISoftDeletable
{
    public bool IsDeleted { get; set; }
}