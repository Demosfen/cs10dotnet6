using System.Text.Json.Serialization;

namespace WMS.Data;

/// <summary>
/// Box
/// </summary>
public sealed class Box : StorageUnit
{
    /// <inheritdoc />
    [JsonInclude]
    public override decimal Weight { get; }

    /// <summary>
    /// Unit production date/time
    /// </summary>
    [JsonInclude]
    public DateTime? ProductionDate { get; }
    
    /// <inheritdoc />
    [JsonInclude]
    public override DateTime? ExpiryDate { get; }

    [JsonConstructor]
    public Box(
        decimal width, 
        decimal height, 
        decimal depth, 
        decimal weight, 
        DateTime? productionDate = null, 
        DateTime? expiryDate = null) 
        : base(width, height, depth)
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
        else
        {
            ExpiryDate = expiryDate;
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