using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

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

    Task<HttpResponseMessage> PostAsync(
        Guid warehouseId, 
        WarehouseRequest request,
        CancellationToken cancellationToken = default);

    Task<HttpResponseMessage> PutAsync(Guid warehouseId,
        WarehouseRequest request,
        CancellationToken cancellationToken = default);

    Task<HttpResponseMessage> DeleteAsync(Guid warehouseId, CancellationToken cancellationToken = default);

}