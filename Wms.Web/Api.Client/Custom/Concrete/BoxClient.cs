using System.Net.Http.Json;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Concrete;

internal sealed class BoxClient : IBoxClient
{
    private readonly HttpClient _client;
    private const string BaseUrl = "/api/v1/";

    public BoxClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyCollection<BoxResponse>?> GetAllAsync(
        Guid paletteId,
        int? offset, int? size,
        CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<IReadOnlyCollection<BoxResponse>>(
            $"{BaseUrl}palettes/{paletteId}/boxes?boxOffset={offset}&boxSize={size}", 
            cancellationToken); 

    public async Task<IReadOnlyCollection<BoxResponse>?> GetAllDeletedAsync(
        Guid paletteId, 
        int? offset, int? size, 
        CancellationToken cancellationToken)
    => await _client.GetFromJsonAsync<IReadOnlyCollection<BoxResponse>>(
        $"{BaseUrl}palettes/{paletteId}/boxes/archive?boxOffset={offset}&boxSize={size}", 
        cancellationToken);
    
    // public async Task<PaletteResponse?> PostAsync(Guid paletteId, Guid boxId, BoxRequest request, CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task<PaletteResponse?> PutAsync(Guid boxId, Guid paletteId, BoxRequest request, CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task<HttpResponseMessage> DeleteAsync(Guid boxId, CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }
}