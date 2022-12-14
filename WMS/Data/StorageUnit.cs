namespace WMS.Data;

/// <summary>
/// This is an abstract class, which contains common properties
/// for rectangle objects of a warehouse.
/// </summary>
public abstract class StorageUnit
{
    /// <summary>
    /// Unit default expiry days
    /// </summary>
    protected const int ExpiryDays = 100;
    
    /// <summary>
    /// Unit ID
    /// </summary>
    public Guid Id { get; }
    
    /// <summary>
    /// Object width
    /// </summary>
    public decimal Width { get; }

    /// <summary>
    /// Object height
    /// </summary>
    public decimal Height { get; }

    /// <summary>
    /// Object depth
    /// </summary>
    public decimal Depth { get; }

    /// <summary>
    /// Unit volume
    /// </summary>
    public virtual decimal Volume => Width * Height * Depth;

    /// <summary>
    /// Object weight, which can be calculated or set
    /// during runtime
    /// </summary>
    public abstract decimal Weight { get; }

    /// <summary>
    /// Unit production date/time
    /// </summary>
    public DateTime? ProductionDate { get; set; }
    
    /// <summary>
    /// Unit expiry date/time
    /// </summary>
    public virtual DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Constructor which strictly encourage developers to
    /// initialize basic properties of storage unit.
    /// </summary>
    /// <param name="width">Unit width</param>
    /// <param name="height">Unit height</param>
    /// <param name="depth">Unit depth</param>
    /// <param name="weight">Unit weight</param>
    /// <param name="productionDate">Unit production Date/Time</param>
    /// <param name="expiryDate">Unit expiry date</param>
    protected StorageUnit(
        decimal width,
        decimal height,
        decimal depth,
        decimal weight,
        DateTime? productionDate = null,
        DateTime? expiryDate = null)
    {
        Id = Guid.NewGuid();
        Width = width;
        Height = height;
        Depth = depth;
        ProductionDate = productionDate;
    }
}