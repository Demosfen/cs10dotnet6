namespace Wms.Web.Store.Entities.Concrete;

/// <summary>
/// Palette class
/// </summary>
public sealed class Palette : StorageUnit
{
   /// <summary>
    /// ID of the warehouse where palette is stored
    /// </summary>
    public required Guid WarehouseId { get; init; }
    
    /// <summary>
    /// Empty palette weight
    /// </summary>
    public const decimal DefaultWeight = 30;
    
    /// <summary>
    /// Boxes on the palette
    /// </summary>
    public List<Box> Boxes { get; set; } = new();
    
    /// <summary>
    /// Palette weight computed as
    /// empty palette weight and
    /// sum of boxes weight
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Palette expiry date is computed as
    /// the minimal box expiry date.
    /// </summary>
    public override DateTime? ExpiryDate { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        if (Boxes is { Count: 0 })
        {
            return $"Palette contains no boxes.";
        }

        var msg = $"Palette {Id}:\n" +
                  $"Warehouse ID: {WarehouseId}, \n" +
                  $"Boxes count: {Boxes!.Count},\n" +
                  $"WxHxD: {Width}x{Height}x{Depth},\n" +
                  $"Volume: {Volume},\n" +
                  $"Weight: {Weight},\n" +
                  $"Expiry Date: {ExpiryDate},\n";
        return msg;
    }
}