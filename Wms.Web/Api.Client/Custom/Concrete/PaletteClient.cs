using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Concrete;

internal sealed class PaletteClient : IPaletteClient
{
    private readonly HttpClient _client;

    public PaletteClient(HttpClient client, IOptions<WmsClientOptions> options)
    {
        _client = client;
    }
    
    public async Task<IReadOnlyCollection<PaletteResponse>?> GetAllAsync(
        Guid warehouseId,
        int? offset, int? size, 
        CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<IReadOnlyCollection<PaletteResponse>>(
            $"warehouses/{warehouseId}/palettes?" +
            $"{nameof(offset)}={offset}&{nameof(size)}={size}", 
            cancellationToken);

    public async Task<IReadOnlyCollection<PaletteResponse>?> GetAllDeletedAsync(
        Guid warehouseId,
        int? offset, int? size, 
        CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<IReadOnlyCollection<PaletteResponse>>(
            $"warehouses/{warehouseId}/palettes/archive?" +
            $"{nameof(offset)}={offset}&{nameof(size)}={size}", 
            cancellationToken);

    public async Task<PaletteResponse?> PostAsync(
        Guid warehouseId,
        Guid paletteId, 
        PaletteRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _client.PostAsJsonAsync(
            $"warehouses/{warehouseId}/palettes/{paletteId}", 
            request, 
            cancellationToken);
        
        return await result.Content.ReadFromJsonAsync<PaletteResponse>(cancellationToken: cancellationToken);
    }

    public async Task<PaletteResponse?> PutAsync(
        Guid paletteId, 
        Guid warehouseId,
        PaletteRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _client.PutAsJsonAsync(
            $"palettes/{paletteId}?" +
            $"{nameof(warehouseId)}={warehouseId}", 
            request, 
            cancellationToken);
        
        return await result.Content.ReadFromJsonAsync<PaletteResponse>(cancellationToken: cancellationToken);
    }
    
    public async Task<HttpResponseMessage> DeleteAsync(Guid paletteId, CancellationToken cancellationToken)
        => await _client.DeleteAsync($"palettes/{paletteId}", cancellationToken);
}