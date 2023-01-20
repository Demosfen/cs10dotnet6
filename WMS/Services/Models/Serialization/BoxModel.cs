namespace WMS.Services.Models.Serialization;

public class BoxModel
{
    public BoxModel(
        decimal weight, 
        DateTime productionDate, 
        DateTime expiryDate, 
        decimal volume, 
        Guid id, 
        decimal width, 
        decimal height, 
        decimal depth)
    {
        Weight = weight;
        ProductionDate = productionDate;
        ExpiryDate = expiryDate;
        Volume = volume;
        Id = id;
        Width = width;
        Height = height;
        Depth = depth;
    }

    public decimal Weight { get; }
    public DateTime ProductionDate { get; }
    public DateTime ExpiryDate { get; }
    public decimal Volume { get; }
    public Guid Id { get; }
    public decimal Width { get; }
    public decimal Height { get; }
    public decimal Depth { get; }
}