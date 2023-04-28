using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Contracts.Requests;

public class UpdateWarehouseRequest
{
    [FromRoute(Name = "warehouseId")]
    public required Guid Id { get; init; }
    
    [FromBody]
    public required WarehouseRequest WarehouseRequest { get; init; }
}