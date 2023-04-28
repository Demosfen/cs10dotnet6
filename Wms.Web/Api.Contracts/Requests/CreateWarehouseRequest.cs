using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Contracts.Requests;

public sealed class CreateWarehouseRequest
{
    [FromRoute(Name = "warehouseId")]
    public required Guid Id { get; init; }
    
    public required WarehouseRequest WarehouseRequest { get; init; }
}