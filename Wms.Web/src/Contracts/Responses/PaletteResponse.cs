namespace Wms.Web.Contracts.Responses;

/// <summary>
/// Palette dto (contract) for read operations
/// </summary>
public sealed class PaletteResponse
{
    /// <summary>
    /// Palette ID
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Warehouse ID where palette located
    /// </summary>
    public required Guid WarehouseId { get; init; }

    /// <summary>
    /// Palette width
    /// </summary>
    public required decimal Width { get; set; }
    
    /// <summary>
    /// Palette height
    /// </summary>
    public required decimal Height { get; set; }

    /// <summary>
    /// Palette depth
    /// </summary>
    public required decimal Depth { get; set; }

    /// <summary>
    /// Palette weight
    /// </summary>
    public required decimal Weight { get; set; }
    
    /// <summary>
    /// Palette volume
    /// </summary>
    public required decimal Volume { get; set; }
    
    /// <summary>
    /// Palette expiry date
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// Collection of boxes (read only) stored on the palette
    /// </summary>
    public IReadOnlyCollection<BoxResponse> Boxes { get; set; } = Array.Empty<BoxResponse>();
}