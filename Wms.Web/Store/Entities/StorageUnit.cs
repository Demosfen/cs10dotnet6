using Wms.Web.Store.Interfaces;

namespace Wms.Web.Store.Entities;

/// <summary>
/// This is an abstract class, which contains common properties
/// for rectangle objects of a warehouse.
/// </summary>
public abstract class StorageUnit: IEntityWithId, IAuditableEntity
{
    /// <summary>
    /// Unit default expiry days
    /// </summary>
    protected const int ExpiryDays = 100;

    public required Guid Id { get; init; }
    
    /// <summary>
    /// Unit width
    /// </summary>
    public required decimal Width { get; set;}

    /// <summary>
    /// Unit height
    /// </summary>
    public required decimal Height { get; set; }

    /// <summary>
    /// Unit depth
    /// </summary>
    public required decimal Depth { get; set; }

    /// <summary>
    /// Unit volume
    /// </summary>
    public decimal Volume { get; set; }

    /// <summary>
    /// Soft delete flag
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Unit expiry date/time
    /// </summary>
    public abstract DateTime? ExpiryDate { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
}