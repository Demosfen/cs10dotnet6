using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Contracts.Requests;

public sealed class CreateWarehouseRequest
{
    [FromBody]
    public required Guid Id { get; init; } = Guid.NewGuid();
    
    [FromRoute(Name = "name")]
    public required string Name { get; init; }

    [FromBody] public WarehouseRequest Warehouse { get; set; } = default!;
}