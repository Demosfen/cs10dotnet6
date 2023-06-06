namespace Wms.Web.Contracts.Requests;

/// <summary>
/// Palette dto (contract) for create/update/delete operations
/// </summary>
public sealed class PaletteRequest
{
    /// <summary>
    /// Palette width
    /// </summary>
    public required decimal Width { get; init; }
    
    /// <summary>
    /// Palette height
    /// </summary>
    public required decimal Height { get; init; }

    /// <summary>
    /// Palette depth
    /// </summary>
    public required decimal Depth { get; init; }
}