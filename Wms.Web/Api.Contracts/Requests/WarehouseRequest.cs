using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Contracts.Requests;

public sealed class WarehouseRequest
{
    [FromRoute(Name = "id")]
    public required Guid Id { get; init; } //= Guid.NewGuid();
    
    [FromRoute(Name = "name")]
    public required string Name { get; set; }
    
    public List<PaletteResponse>? Palettes { set; get; }
}