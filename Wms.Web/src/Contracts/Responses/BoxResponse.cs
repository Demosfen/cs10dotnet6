namespace Wms.Web.Contracts.Responses;

/// <summary>
/// Box dto (contract) for read operations
/// </summary>
public sealed class BoxResponse
{
    /// <summary>
    /// Box ID
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Palette ID where box located
    /// </summary>
    public required Guid PaletteId { get; init; }
    
    /// <summary>
    /// Box width
    /// </summary>
    public required decimal Width { get; init; }
    
    /// <summary>
    /// Box height
    /// </summary>
    public required decimal Height { get; init; }

    /// <summary>
    /// Box depth
    /// </summary>
    public required decimal Depth { get; set; }
    
    /// <summary>
    /// Box weight
    /// </summary>
    public required decimal Weight { get; set; }
    
    /// <summary>
    /// Box volume
    /// </summary>
    public required decimal Volume { get; set; }

    /// <summary>
    /// Box production date
    /// </summary>
    public DateTime? ProductionDate { get; set; }
    
    /// <summary>
    /// Box expiry date
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
}