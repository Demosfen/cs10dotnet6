using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Contracts.Requests;

public sealed class CreateWarehouseRequest      //TODO why not to use Warehouse request directly?
{    
    [FromRoute(Name = "id")]
    public required Guid Id { get; init; }
    
    [FromRoute(Name = "name")]
    public required string Name { get; init; }
    
    [FromBody] 
    public WarehouseRequest Warehouse { get; set; } = null!;      //TODO: What happens here?
}