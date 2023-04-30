using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

public sealed class CreatePaletteRequest
{
    public required Guid WarehouseId { get; init; }
    
    public required Guid Id { get; init; }

    public required PaletteRequest PaletteRequest { get; init;}
}