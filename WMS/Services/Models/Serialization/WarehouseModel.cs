using WMS.Data;

namespace WMS.Services.Models.Serialization;

public class RootObject
{
    public RootObject(List<Palette> palettes)
    {
        Palettes = palettes;
    }

    public List<Palette> Palettes { get; }
}

public class Palette
{
    public Palette(
        List<Box> boxes, 
        int volume, 
        int weight, 
        string expiryDate, 
        string id, 
        int width, 
        int height, 
        int depth)
    {
        Boxes = boxes;
        Volume = volume;
        Weight = weight;
        ExpiryDate = expiryDate;
        Id = id;
        Width = width;
        Height = height;
        Depth = depth;
    }

    public List<Box> Boxes { get; }
    public int Volume { get; }
    public int Weight { get; }
    public string ExpiryDate { get; }
    public string Id { get; }
    public int Width { get; }
    public int Height { get; }
    public int Depth { get; }
}

public class Boxes
{
    public Boxes(
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

