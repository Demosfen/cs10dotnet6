using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

public sealed class CreatePaletteRequest
{
    [FromRoute(Name = "warehouseId")]
    public required Guid WarehouseId { get; init; }
    
    [FromRoute(Name = "paletteId")]
    public required Guid Id { get; init; }

    [FromBody]
    public required PaletteRequest PaletteRequest { get; init;}
}