using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Contracts.Requests;

public sealed class CreateWarehouseRequest
{
    [FromRoute(Name = "id")]
    public required Guid Id { get; init; }

    [FromBody] public WarehouseRequest Warehouse { get; set; } = default!;
}