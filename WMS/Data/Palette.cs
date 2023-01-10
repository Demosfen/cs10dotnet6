namespace WMS.Data;

/// <summary>
/// A class describing palette
/// </summary>
public sealed class Palette : StorageUnit
{
    /// <summary>
    /// Empty palette weight
    /// </summary>
    private const decimal DefaultWeight = 30;

    public override decimal Volume
    {
        get
        {
            decimal paletteVolume = 0;

            foreach (var box in Boxes)
            {
                paletteVolume += box.Volume;
            }
            
            return Width*Height*Depth + paletteVolume;
        }
    }

    /// <summary>
    /// Boxes on the palette
    /// </summary>
    public List<Box> Boxes = new List<Box>();

    public override decimal Weight
    {
        get
        {
            decimal boxesWeight = 0;

            foreach (var box in Boxes)
            {
                boxesWeight += box.Weight;
            }
            
            return DefaultWeight + boxesWeight;
        }
    }

    public Palette(
        decimal width,
        decimal height,
        decimal depth,
        decimal weight,
        DateTime? productionDate = null,
        DateTime? expiryDate = null)
        : base(width, height, depth, weight, productionDate, expiryDate) { }
}