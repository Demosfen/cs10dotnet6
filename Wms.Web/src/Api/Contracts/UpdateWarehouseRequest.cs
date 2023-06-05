using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

/// <summary>
/// Contract for updating a warehouse
/// </summary>
public class UpdateWarehouseRequest
{
    /// <summary>
    /// Warehouse ID
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Contract (body) for updating a warehouse
    /// </summary>
    public required WarehouseRequest WarehouseRequest { get; init; }
}