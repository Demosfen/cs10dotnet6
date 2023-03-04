using WMS.WarehouseDbContext.Interfaces;

namespace WMS.WarehouseDbContext.Entities;

public sealed class Palette : StorageUnit
{
    /// <summary>
    /// Navigation property
    /// </summary>
    private Warehouse? _warehouse;
    
    /// <summary>
    /// ID of the warehouse where palette is stored
    /// </summary>
    public Guid WarehouseId { get; set; }
    
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
    ///  Default palette constructor
    /// </summary>
    /// <param name="warehouseId">Warehouse ID storing palette</param>
    /// <param name="width">Palette width</param>
    /// <param name="height">Palette height</param>
    /// <param name="depth">Palette depth</param>
    public Palette(
        Guid warehouseId,
        decimal width,
        decimal height,
        decimal depth)
        : base(width, height, depth)
    {
        WarehouseId = warehouseId;
    }

    public Warehouse Warehouse
    {
        set => _warehouse = value;
        get => _warehouse
               ?? throw new InvalidOperationException("Uninitialized property" + nameof(_warehouse));
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