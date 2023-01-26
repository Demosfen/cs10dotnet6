namespace WMS.Data;

/// <summary>
/// Box
/// </summary>
public sealed class Box : StorageUnit
{
    /// <inheritdoc />
    public override decimal Weight { get; }

    /// <summary>
    /// Unit production date/time
    /// </summary>
    public DateTime? ProductionDate { get; }
    
    /// <inheritdoc />
    public override DateTime? ExpiryDate { get; }
    
    public Box(
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
    }

    public override string ToString()
    {
        return $"The box with ID {Id} has:\n" +
               $"Size (WxHxD): {Width}x{Height}x{Depth} decimeters,\n" +
               $"Volume: {Volume} decimeters,\n" +
               $"Weight: {Weight} kilo(s),\n" +
               $"Production date: {ProductionDate},\n" +
               $"Expiry date: {ExpiryDate}.\n";
    }
}