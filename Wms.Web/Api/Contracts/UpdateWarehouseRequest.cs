using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

public class UpdateWarehouseRequest
{
    public required Guid Id { get; init; }
    
    public required WarehouseRequest WarehouseRequest { get; init; }
}