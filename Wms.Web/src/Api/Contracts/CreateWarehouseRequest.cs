using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

/// <summary>
/// Contract for creating a warehouse
/// </summary>
public sealed class CreateWarehouseRequest
{
    /// <summary>
    /// Warehouse ID
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Contract (body) for crating a warehouse
    /// </summary>
    public required WarehouseRequest WarehouseRequest { get; init; }
}