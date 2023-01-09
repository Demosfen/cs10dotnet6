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
        DateTime? productionDate = null,
        DateTime? expiryDate = null,
        double expiryDays = 100)
        : base(width, height, depth, weight, productionDate, expiryDate, expiryDays) { }
}