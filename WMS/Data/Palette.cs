namespace WMS.Data;

/// <summary>
/// A class describing palette
/// </summary>
public sealed class Palette : StorageUnit
{
    /// <summary>
    /// Empty palette weight
    /// </summary>
    private const decimal _defaultWeight = 30;

    public override decimal Volume { get; }

    public override decimal Weight
    {
        get
        {
            if (Boxes != null)
            {
                decimal boxesWeight = 0;
        
                foreach (var box in Boxes)
                {
                    boxesWeight += box.Weight;
                }

                return boxesWeight + _defaultWeight;
            }
            else
            {
                return _defaultWeight;
            }
        }
    }

    /// <summary>
    /// Boxes on the palette
    /// </summary>
    public List<Box>? Boxes = new List<Box>();

    /// <inheritdoc />
    public Palette(
        decimal width,
        decimal height,
        decimal depth,
        decimal weight,
        DateTime? productionDate = null,
        DateTime? expiryDate = null)
        : base(width, height, depth, weight, productionDate, expiryDate)
    {
        Weight = _defaultWeight;
    }
}