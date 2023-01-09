using static System.Console;

namespace WMS.Data;

/// <summary>
/// This is an abstract class, which contains common properties
/// for rectangle objects of a warehouse.
/// </summary>
public abstract class StorageUnit
{
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
    /// Unit volume. Computed automatically
    /// </summary>
    public decimal Volume { get; }

    /// <summary>
    /// Object weight, which can be calculated or set
    /// during runtime
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Unit production date/time
    /// </summary>
    public DateTime? ProductionDate { get; }
    
    /// <summary>
    /// Unit expiry date/time
    /// </summary>
    public DateTime? ExpiryDate { get; }
    
    /// <summary>
    /// Unit expiry days number
    /// </summary>
    public double ExpiryDays { get; }

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
    /// <param name="expiryDays">Unit expiry days</param>
    protected StorageUnit(
        decimal width,
        decimal height,
        decimal depth,
        decimal weight,
        DateTime? productionDate = null,
        DateTime? expiryDate = null,
        double expiryDays = 100)
    {
        Id = Guid.NewGuid();
        Width = width;
        Height = height;
        Depth = depth;
        Volume = Width * Height * Depth;
        Weight = weight;
        ExpiryDays = expiryDays;
        
        if (expiryDate == null && productionDate == null)
            throw new ArgumentException(
                "Both Production and Expiry dates shouldn't be null simultaneously");
        
        ExpiryDate = expiryDate ?? productionDate!.Value.AddDays(expiryDays);

        ProductionDate = productionDate;

        if (ExpiryDate <= ProductionDate)
            throw new ArgumentException(
                "Expiry date cannot be lower than Production date!", 
                ExpiryDate.ToString());
    }
}