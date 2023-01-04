namespace WMS.Data;

/// <summary>
/// A class describing palette
/// </summary>
public class Palette : StorageUnit
{
    public List<Box>? Boxes = new List<Box>();

    public Palette
        (decimal width, 
            decimal height, 
            decimal depth, 
            decimal weight) 
        : base(width, height, depth, weight) {}
}