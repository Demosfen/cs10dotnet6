using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Concrete;

internal sealed class WarehouseClient : IWarehouseClient
{
    private readonly HttpClient _client;
    private readonly Uri? _requestUri;

    public WarehouseClient(HttpClient client, IOptions<WmsClientOptions> options)
    {
        _client = client;
        _requestUri = options.Value.WarehouseClientBaseUrl;
    }

    public async Task<IReadOnlyCollection<WarehouseResponse>?> GetAllAsync(
        int? offset, int? size, CancellationToken cancellationToken)
    
        => await _client.GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>(
            $"{_requestUri}?" +
            $"{nameof(offset)}={offset}&{nameof(size)}={size}", 
            cancellationToken);

    public async Task<IReadOnlyCollection<WarehouseResponse>?> GetAllDeletedAsync(
        int? offset, int? size, CancellationToken cancellationToken)
    
    => await _client.GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>(
            $"{_requestUri}/archive?" +
            $"{nameof(offset)}={offset}&{nameof(size)}={size}", 
            cancellationToken);

    public async Task<WarehouseResponse?> PostAsync(
        Guid warehouseId, 
        WarehouseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _client.PostAsJsonAsync(
            $"{_requestUri}?warehouseId={warehouseId}", 
            request, 
            cancellationToken);
        
        return await result.Content.ReadFromJsonAsync<WarehouseResponse>(cancellationToken: cancellationToken);
    }
    
    public async Task<WarehouseResponse?> PutAsync(
        Guid warehouseId, 
        WarehouseRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _client.PutAsJsonAsync(
            $"{_requestUri}/{warehouseId}", 
            request, 
            cancellationToken);
        
        return await result.Content.ReadFromJsonAsync<WarehouseResponse>(cancellationToken: cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid warehouseId, CancellationToken cancellationToken)
    => await _client.DeleteAsync($"{_requestUri}/{warehouseId}", cancellationToken);

}