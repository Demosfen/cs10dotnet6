using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Concrete;

internal sealed class BoxClient : IBoxClient
{
    private const string Ver1 = "/api/v1/";
    
    private readonly HttpClient _client;

    public BoxClient(HttpClient client, IOptions<WmsClientOptions> options)
    {
        _client = client;
    }

    public async Task<IReadOnlyCollection<BoxResponse>?> GetAllAsync(
        Guid paletteId,
        int? offset, int? size,
        CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<IReadOnlyCollection<BoxResponse>>(
            $"{Ver1}palettes/{paletteId}/boxes?offset={offset}&size={size}", 
            cancellationToken); 

    public async Task<IReadOnlyCollection<BoxResponse>?> GetAllDeletedAsync(
        Guid paletteId, 
        int? offset, int? size, 
        CancellationToken cancellationToken)
    => await _client.GetFromJsonAsync<IReadOnlyCollection<BoxResponse>>(
        $"{Ver1}palettes/{paletteId}/boxes/archive?" +
        $"offset={offset}&size={size}", 
        cancellationToken);
    
    public async Task<BoxResponse?> CreateAsync(Guid paletteId,
        Guid boxId,
        BoxRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _client.PostAsJsonAsync(
            $"{Ver1}palettes/{paletteId}?boxId={boxId}",
            request,
            cancellationToken);

        var a = await result.Content.ReadAsStringAsync(CancellationToken.None);

        return await result.Content.ReadFromJsonAsync<BoxResponse>(cancellationToken: cancellationToken);
    }
        
    
    
    public async Task<BoxResponse?> PutAsync(
        Guid boxId, 
        Guid paletteId, 
        BoxRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _client.PutAsJsonAsync(
            $"{Ver1}boxes/{boxId}?" +
            $"paletteId={paletteId}",
            request, cancellationToken);

        return await result.Content.ReadFromJsonAsync<BoxResponse>(cancellationToken: cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid boxId, CancellationToken cancellationToken)
        => await _client.DeleteAsync($"{Ver1}boxes/{boxId}", cancellationToken);
}