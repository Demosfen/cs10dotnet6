using static System.Console;

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
    protected const double ExpiryDays = 100;
    
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
    public virtual decimal Weight { get; }

    /// <summary>
    /// Unit production date/time
    /// </summary>
    public DateTime? ProductionDate { get; }
    
    /// <summary>
    /// Unit expiry date/time
    /// </summary>
    public DateTime? ExpiryDate { get; }

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
        Weight = weight;

        if (expiryDate == null && productionDate == null)
        {
            throw new ArgumentException(
                "Both Production and Expiry dates shouldn't be null simultaneously",
                paramName: nameof(expiryDate));
        }

        if (productionDate != null)
        {
            ProductionDate = productionDate;
            
            ExpiryDate = expiryDate ?? 
                         productionDate.Value.AddDays(ExpiryDays);
        }

        if (ExpiryDate <= ProductionDate)
        {
            throw new ArgumentException(
                "Expiry date cannot be lower than Production date!", 
                paramName: nameof(ExpiryDate));
        }
    }
}