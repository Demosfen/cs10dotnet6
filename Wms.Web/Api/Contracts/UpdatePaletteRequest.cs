using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

public sealed class UpdatePaletteRequest
{
    [FromRoute(Name = "paletteId")]
    public required Guid Id { get; init; }
    
    [FromQuery]
    public required Guid WarehouseId { get; init; }

    [FromBody] public required PaletteRequest PaletteRequest { get; init; }
}