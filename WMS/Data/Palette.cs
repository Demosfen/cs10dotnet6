namespace WMS.Data;

/// <summary>
/// A class describing palette
/// </summary>
public class Palette : StorageUnit
{
    public List<Box>? Boxes = new List<Box>();

    public Palette(
        decimal width, 
        decimal height, 
        decimal depth, 
        decimal weight, 
        DateTime production = default,
        DateTime expiryDate = default,
        int expiryDays = default)
        : base(width, height, depth, weight, production, expiryDate, expiryDays) { }
}