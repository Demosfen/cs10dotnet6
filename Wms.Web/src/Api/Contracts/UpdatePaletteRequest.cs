using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

public sealed class UpdatePaletteRequest
{
    public required Guid Id { get; init; }
    
    public required Guid WarehouseId { get; init; }

    public required PaletteRequest PaletteRequest { get; init; }
}