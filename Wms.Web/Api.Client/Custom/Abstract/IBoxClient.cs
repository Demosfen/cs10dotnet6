using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Abstract;

public interface IBoxClient
{
    Task<IReadOnlyCollection<BoxResponse>?> GetAllAsync(
        Guid paletteId,
        int? offset, int? size, 
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<BoxResponse>?> GetAllDeletedAsync(
        Guid paletteId,
        int? offset, int? size, 
        CancellationToken cancellationToken = default);
        
    Task<BoxResponse?> CreateAsync(Guid paletteId,
        Guid boxId,
        BoxRequest request,
        CancellationToken cancellationToken = default);
        
    Task<BoxResponse?> PutAsync(
        Guid boxId,
        Guid paletteId,
        BoxRequest request,
        CancellationToken cancellationToken = default);
        
    Task<HttpResponseMessage> DeleteAsync(Guid boxId, CancellationToken cancellationToken = default);
    
}