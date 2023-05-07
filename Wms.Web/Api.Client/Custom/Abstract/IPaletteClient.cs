using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Abstract;

public interface IPaletteClient
    {
        Task<IReadOnlyCollection<PaletteResponse>?> GetAllAsync(
            Guid warehouseId,
            int? offset, int? size, 
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<PaletteResponse>?> GetAllDeletedAsync(
            Guid warehouseId,
            int? offset, int? size, 
            CancellationToken cancellationToken = default);
        
        Task<PaletteResponse?> PostAsync(
            Guid warehouseId,
            Guid paletteId, 
            PaletteRequest request,
            CancellationToken cancellationToken = default);
        
        Task<PaletteResponse?> PutAsync(
            Guid paletteId,
            Guid warehouseId,
            PaletteRequest request,
            CancellationToken cancellationToken = default);
        
        Task<HttpResponseMessage> DeleteAsync(Guid paletteId, CancellationToken cancellationToken = default);

    }