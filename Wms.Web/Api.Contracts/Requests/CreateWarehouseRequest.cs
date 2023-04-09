namespace Wms.Web.Api.Contracts.Requests;

public sealed class CreateWarehouseRequest
{
    public required Guid Id { get; set; }
    
    public required string Name { get; init; }
}