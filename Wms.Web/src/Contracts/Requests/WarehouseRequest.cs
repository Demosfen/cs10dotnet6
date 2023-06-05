namespace Wms.Web.Contracts.Requests;

/// <summary>
/// Warehouse dto (contract) for create/update/delete operations
/// </summary>
public class WarehouseRequest
{
    /// <summary>
    /// Warehouse name (lenght should be lower than 40 chars)
    /// </summary>
    public required string Name { get; init; }
}