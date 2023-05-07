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

    Task<WarehouseResponse?> PostAsync(
        Guid warehouseId, 
        WarehouseRequest request,
        CancellationToken cancellationToken = default);

    Task<WarehouseResponse?> PutAsync(
        Guid warehouseId,
        WarehouseRequest request,
        CancellationToken cancellationToken = default);

    Task<HttpResponseMessage> DeleteAsync(Guid warehouseId, CancellationToken cancellationToken = default);

}