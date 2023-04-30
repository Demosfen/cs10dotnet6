using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

public sealed class CreateWarehouseRequest
{
    [FromRoute(Name = "warehouseId")]
    public required Guid Id { get; init; }
    
    public required WarehouseRequest WarehouseRequest { get; init; }
}