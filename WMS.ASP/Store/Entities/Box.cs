using WMS.ASP.Common.Exceptions;

namespace WMS.ASP.Store.Entities;

public sealed class Box : StorageUnit
{
    private Palette? _palette;
   
    /// <summary>
    /// Navigation property
    /// ID of the palette where box is stored
    /// </summary>
    public Guid PaletteId { get; set; }

    /// <inheritdoc cref="StorageUnit" />
    public decimal Weight { get; set; }

    /// <summary>
    /// Unit production date/time
    /// </summary>
    public DateTime? ProductionDate { get; set; }
    
    /// <inheritdoc />
    public override DateTime? ExpiryDate { get; set; }

    public Palette Palette
    {
        set => _palette = value;
        get => _palette
               ?? throw new UninitializedPropertyException(nameof(Box));
    }

    /// <summary>
    /// Box constructor
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public Box(
        Guid paletteId,
        decimal width, 
        decimal height, 
        decimal depth, 
        decimal weight, 
        DateTime? productionDate = null, 
        DateTime? expiryDate = null) 
        : base(width, height, depth)
    {
        if (weight <= 0)
        {
            throw new ArgumentException("Unit weight shouldn't be less or equal zero!");
        }
        
        Weight = weight;
        
        if (expiryDate == null && productionDate == null)
        {
            throw new ArgumentException(
                "Both Production and Expiry dates shouldn't be null simultaneously");
        }

        if (productionDate != null)
        {
            ProductionDate = productionDate;
            
            ExpiryDate = expiryDate ?? 
                         productionDate.Value.AddDays(ExpiryDays);
        }
        else
        {
            ExpiryDate = expiryDate;
        }

        if (ExpiryDate <= ProductionDate)
        {
            throw new ArgumentException(
                "Expiry date cannot be lower than Production date!");
        }

        PaletteId = paletteId;
    }
    
    public override string ToString()
    {
        return $"Box ID: {Id},\n" +
               $"Size (WxHxD): {Width}x{Height}x{Depth},\n" +
               $"Volume: {Volume},\n" +
               $"Weight: {Weight},\n" +
               $"Production date: {ProductionDate},\n" +
               $"Expiry date: {ExpiryDate}.\n";
    }
}