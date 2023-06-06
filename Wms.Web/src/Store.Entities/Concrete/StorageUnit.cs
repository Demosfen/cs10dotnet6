using Wms.Web.Store.Entities.Interfaces;

namespace Wms.Web.Store.Entities.Concrete;

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
    /// Unit expiry date/time
    /// </summary>
    public abstract DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Entity creation UTC time
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Entity update UTC time
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Entity delete UTC time
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}