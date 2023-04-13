using Wms.Web.Common.Exceptions;

namespace Wms.Web.Store.Entities;

public sealed class Palette : StorageUnit
{
    // /// <summary>
    // /// Navigation property
    // /// </summary>
    // private Warehouse? _warehouse;
    
    /// <summary>
    /// ID of the warehouse where palette is stored
    /// </summary>
    public required Guid WarehouseId { get; set; }
    
    /// <summary>
    /// Empty palette weight
    /// </summary>
    public const decimal DefaultWeight = 30;
    
    /// <summary>
    /// Boxes on the palette
    /// </summary>
    public List<Box>? Boxes { get; } = new();
    
    /// <summary>
    /// Palette weight computed as
    /// empty palette weight and
    /// sum of boxes weight
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Palette expiry date is computed as
    /// the minimal box exp. date.
    /// </summary>
    public override DateTime? ExpiryDate { get; set; }

    // /// <summary>
    // ///  Default palette constructor
    // /// </summary>
    // public Palette(
    //     Guid warehouseId,
    //     decimal width,
    //     decimal height,
    //     decimal depth)
    //     : base(width, height, depth)
    // {
    //     WarehouseId = warehouseId;
    // }

    // /// <summary>
    // /// Navigation property
    // /// </summary>
    // /// <exception cref="UninitializedPropertyException">Property uninitialized</exception>
    // public Warehouse Warehouse
    // {
    //     set => _warehouse = value;
    //     get => _warehouse
    //            ?? throw new UninitializedPropertyException(nameof(Warehouse));
    // }

    public override string ToString()
    {
        if (Boxes is { Count: 0 })
        {
            return $"Palette contains no boxes.";
        }

        var msg = $"Palette {Id}:\n" +
                  $"Boxes count: {Boxes!.Count},\n" +
                  $"WxHxD: {Width}x{Height}x{Depth},\n" +
                  $"Volume: {Volume},\n" +
                  $"Weight: {Weight},\n" +
                  $"Expiry Date: {ExpiryDate},\n";
        return msg;
    }
}