using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

/// <summary>
/// Contract for updating a palette
/// </summary>
public sealed class UpdatePaletteRequest
{
    /// <summary>
    /// Palette ID
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Warehouse ID where palette is located
    /// </summary>
    public required Guid WarehouseId { get; init; }

    /// <summary>
    /// Contract (body) for updating a palette
    /// </summary>
    public required PaletteRequest PaletteRequest { get; init; }
}