using Wms.Web.Common.Exceptions;
using Wms.Web.Contracts.Requests;
using Wms.Web.Contracts.Responses;

namespace Wms.Web.Client.Custom.Abstract;

public interface IWarehouseClient
{
    /// <summary>
    /// Get all warehouses
    /// </summary>
    /// <param name="offset">Warehouse offset</param>
    /// <param name="size">Warehouse collection size</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns></returns>
    Task<IReadOnlyCollection<WarehouseResponse>?> GetAllAsync(
        int? offset, int? size, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all deleted warehouses
    /// </summary>
    /// <param name="offset">Warehouse offset</param>
    /// <param name="size">Warehouse collection size</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns></returns>
    Task<IReadOnlyCollection<WarehouseResponse>?> GetAllDeletedAsync(
        int? offset, int? size, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get warehouse by Id
    /// </summary>
    /// <param name="warehouseId">Warehouse ID</param>
    /// <param name="offset">Palette offset</param>
    /// <param name="size">Palette collection size</param>
    /// <param name="cancellationToken">Token</param>
    /// <exception cref="EntityNotFoundException">Warehouse does not exist</exception>>
    /// <returns>Warehouse entity</returns>
    Task<WarehouseResponse?> GetByIdAsync(
        Guid warehouseId,
        int? offset, int? size,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create warehouse
    /// </summary>
    /// <exception cref="EntityAlreadyExistException">In case of double entities</exception>>
    /// <exception cref="ApiValidationException">BadRequest exception</exception>>
    Task<WarehouseResponse?> CreateAsync(
        Guid warehouseId,
        WarehouseRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates warehouse
    /// </summary>
    /// <param name="warehouseId">Warehouse ID</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns>Updated warehouse entity</returns>
    Task<WarehouseResponse?> PutAsync(Guid warehouseId,
        WarehouseRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes warehouse
    /// </summary>
    /// <param name="warehouseId">Warehouse ID</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns>HttpResponseMessage</returns>
    Task<HttpResponseMessage> DeleteAsync(Guid warehouseId, CancellationToken cancellationToken = default);
}