namespace WMS.Data;

/// <summary>
/// A class describing palette
/// </summary>
public class Palette : StorageUnit
{
    public List<Box>? Boxes = new List<Box>();

    public Palette(string id, decimal width, decimal height, decimal depth, decimal weight) : base(id, width, height, depth, weight)
    {
    }

    public Palette() : base(BASE)
    {
        throw new NotImplementedException();
    }
}