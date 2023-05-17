using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Common.Exceptions;

namespace Wms.Web.Api.Client.Custom.Abstract;

public interface IWarehouseClient
{
    Task<IReadOnlyCollection<WarehouseResponse>?> GetAllAsync(
        int? offset, int? size, 
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<WarehouseResponse>?> GetAllDeletedAsync(
        int? offset, int? size, 
        CancellationToken cancellationToken = default);

    Task<WarehouseResponse?> GetByIdAsync(
        Guid warehouseId,
        int? offset, int? size,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// This method creates warehouse
    /// </summary>
    /// <exception cref="EntityAlreadyExistException"></exception>>
    Task<WarehouseResponse?> CreateAsync(
        Guid warehouseId,
        WarehouseRequest request,
        CancellationToken cancellationToken = default);

    Task<HttpResponseMessage> PutAsync(Guid warehouseId,
        WarehouseRequest request,
        CancellationToken cancellationToken = default);

    Task<HttpResponseMessage> DeleteAsync(Guid warehouseId, CancellationToken cancellationToken = default);

}