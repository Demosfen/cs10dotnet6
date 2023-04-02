using Wms.Web.Store.Interfaces;

namespace Wms.Web.Store.Entities;

/// <summary>
/// This is an abstract class, which contains common properties
/// for rectangle objects of a warehouse.
/// </summary>
public abstract class StorageUnit: IEntityWithId, ISoftDeletable
{
    /// <summary>
    /// Unit default expiry days
    /// </summary>
    public const int ExpiryDays = 100;
    
    /// <summary>
    /// Unit ID
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Unit width
    /// </summary>
    public decimal Width { get; set;}

    /// <summary>
    /// Unit height
    /// </summary>
    public decimal Height { get; set; }

    /// <summary>
    /// Unit depth
    /// </summary>
    public decimal Depth { get; set; }

    /// <summary>
    /// Unit volume
    /// </summary>
    public decimal Volume { get; set; }

    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Unit expiry date/time
    /// </summary>
    public abstract DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Constructor which strictly encourage developers to
    /// initialize basic properties of storage unit.
    /// </summary>
    /// <param name="width">Unit width</param>
    /// <param name="height">Unit height</param> 
    /// <param name="depth">Unit depth</param>
    protected StorageUnit(
        decimal width,
        decimal height,
        decimal depth)
    {
        if (width <= 0 | height <=0 | depth <= 0)
        {
            throw new ArgumentException("Unit size (Height, Width or Depth) shouldn't be less or equal zero!");
        }

        Id = Guid.NewGuid();
        Width = width;
        Height = height;
        Depth = depth;
        Volume = width * height * depth;
    }
}