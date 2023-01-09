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
    public decimal Weight { get; }

    /// <summary>
    /// Unit production date/time
    /// </summary>
    public DateTime Production { get; }
    
    /// <summary>
    /// Unit expiry date/time
    /// </summary>
    public DateTime ExpiryDate { get; }
    
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
    /// <param name="production">Unit production Date/Time</param>
    /// <param name="expiryDate">Unit expiry date</param>
    /// <param name="expiryDays">Unit expiry days</param>
    protected StorageUnit(
        decimal width,
        decimal height,
        decimal depth,
        decimal weight,
        DateTime? production = null,
        DateTime? expiryDate = null,
        double expiryDays = 100)
    {
        Id = Guid.NewGuid();
        Width = width;
        Height = height;
        Depth = depth;
        Weight = weight;
        ExpiryDays = expiryDays;
        
        WriteLine(production);

        if (production is null)
        {
            WriteLine("Production Date is null...");
        }
    }
    /*
    if (production is null)
    Production = production ?? throw new ArgumentNullException(message:
            "Both Expiry Date and Production Date is null. You should type one of them.", 
            paramName: production.ToString());
    ExpiryDate = Production.Value.AddDays(expiryDays);
}*/
}