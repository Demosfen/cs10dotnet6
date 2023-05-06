using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Abstract;

    internal interface IPaletteClient
    {
        Task<IReadOnlyCollection<PaletteResponse>?> GetAllAsync(
            Guid warehouseId,
            int? offset, int? size, 
            CancellationToken cancellationToken = default);

        // Task<IReadOnlyCollection<PaletteResponse>?> GetAllDeletedAsync(
        //     int? offset, int? size, 
        //     CancellationToken cancellationToken = default);
        //
        // Task<WarehouseResponse?> PostAsync(
        //     Guid paletteId, 
        //     PaletteRequest request,
        //     CancellationToken cancellationToken = default);
        //
        // Task<WarehouseResponse?> PutAsync(
        //     Guid paletteId,
        //     PaletteRequest request,
        //     CancellationToken cancellationToken = default);
        //
        // Task<HttpResponseMessage> DeleteAsync(Guid paletteId, CancellationToken cancellationToken = default);

    }