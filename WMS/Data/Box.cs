namespace WMS.Data;

public sealed class Box : StorageUnit
{
    /// <summary>
    /// Box weight
    /// </summary>
    public override decimal Weight { get; }
    
    public Box(
        decimal width, 
        decimal height, 
        decimal depth, 
        decimal weight, 
        DateTime? productionDate = null, 
        DateTime? expiryDate = null) 
        : base(width, height, depth, weight, productionDate, expiryDate)
    {
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