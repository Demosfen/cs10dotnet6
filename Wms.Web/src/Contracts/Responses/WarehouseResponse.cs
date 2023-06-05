namespace Wms.Web.Contracts.Responses;

/// <summary>
/// Warehouse dto (contract) for read operations
/// </summary>
public sealed class WarehouseResponse
{
    /// <summary>
    /// Warehouse ID
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Warehouse name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Collection of the palettes (read only) stored inside the warehouse
    /// </summary>
    public IReadOnlyCollection<PaletteResponse> Palettes { set; get; } = Array.Empty<PaletteResponse>();
}