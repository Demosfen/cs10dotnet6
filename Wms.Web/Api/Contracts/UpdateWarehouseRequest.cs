using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

public class UpdateWarehouseRequest
{
    [FromRoute(Name = "warehouseId")]
    public required Guid Id { get; init; }
    
    [FromBody]
    public required WarehouseRequest WarehouseRequest { get; init; }
}