namespace WMS.Services.Models.Serialization;

public class BoxModel
{
    public BoxModel(
        int weight, 
        string productionDate, 
        string expiryDate, 
        int volume, 
        string id, 
        int width, 
        int height, 
        int depth)
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

    public int Weight { get; }
    public string ProductionDate { get; }
    public string ExpiryDate { get; }
    public int Volume { get; }
    public string Id { get; }
    public int Width { get; }
    public int Height { get; }
    public int Depth { get; }
}