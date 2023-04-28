using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Contracts.Requests;

public sealed class UpdatePaletteRequest
{
    [FromRoute(Name = "paletteId")]
    public required Guid Id { get; init; }
    
    [FromQuery]
    public required Guid WarehouseId { get; init; }

    [FromBody] public required PaletteRequest PaletteRequest { get; init; }
}