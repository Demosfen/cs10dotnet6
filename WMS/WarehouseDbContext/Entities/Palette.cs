namespace WMS.WarehouseDbContext.Entities;

public sealed record Palette : StorageUnit
{
    /// <summary>
    /// Empty palette weight
    /// </summary>
    public const decimal DefaultWeight = 30;
    
    /// <summary>
    /// Boxes on the palette
    /// </summary>
    
    public List<Box> Boxes { get; } = new();
    
    /// <summary>
    /// Palette weight computed as
    /// empty palette weight and
    /// sum of boxes weight
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Palette expiry date computed as
    /// the minimal box exp. date.
    /// </summary>
    public override DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Default palette constructor
    /// </summary>
    /// <param name="width">Palette width</param>
    /// <param name="height">Palette height</param>
    /// <param name="depth">Palette depth</param>
    public Palette(
        decimal width,
        decimal height,
        decimal depth)
        : base(width, height, depth)
    {
    }

    /// <summary>
    /// Output of all information about
    /// boxes on the palette
    /// </summary>
    /// <returns>Palette info</returns>
    public override string ToString()
    {
        if (Boxes.Count == 0)
        {
            return $"Palette contains no boxes.";
        }

        var msg = $"Palette {Id}:\n" +
                  $"Boxes count: {Boxes.Count},\n" +
                  $"WxHxD: {Width}x{Height}x{Depth},\n" +
                  $"Volume: {Volume},\n" +
                  $"Weight: {Weight},\n" +
                  $"Expiry Date: {ExpiryDate},\n";
        return msg;
    }
}