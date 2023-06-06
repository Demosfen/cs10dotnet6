using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Contracts;


/// <summary>
/// Contract for creating a palette
/// </summary>
public sealed class CreatePaletteRequest
{
    /// <summary>
    /// Warehouse ID where palette located 
    /// </summary>
    public required Guid WarehouseId { get; init; }
    
    /// <summary>
    /// Palette ID
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Contract (body) for creating a palette
    /// </summary>
    public required PaletteRequest PaletteRequest { get; init;}
}