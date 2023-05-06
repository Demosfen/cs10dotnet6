using System.Net;
using System.Net.Http.Json;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Concrete;

public class WarehouseClient : IWarehouseClient
{
    private readonly HttpClient _client;
    private const string BaseUrl = "/api/v1/warehouses";

    public WarehouseClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyCollection<WarehouseResponse>?> GetAllAsync(
        int? offset, int? size, CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>(
            $"{BaseUrl}?offset={offset}&size={size}", 
            cancellationToken); //TODO how from here client goes to controller?

    public async Task<IReadOnlyCollection<WarehouseResponse>?> GetAllDeletedAsync(
        int? offset, int? size, CancellationToken cancellationToken)
    => await _client.GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>(
            $"{BaseUrl}/archive?offset={offset}&limit={size}", 
            cancellationToken);

    public async Task<WarehouseResponse?> PostAsync(Guid warehouseId, WarehouseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _client.PostAsJsonAsync(
            $"{BaseUrl}?warehouseId={warehouseId}", 
            request, 
            cancellationToken);
        
        return await result.Content.ReadFromJsonAsync<WarehouseResponse>(cancellationToken: cancellationToken);
    }
    
    public async Task<WarehouseResponse?> PutAsync(Guid warehouseId, WarehouseRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _client.PutAsJsonAsync(
            $"{BaseUrl}/{warehouseId}", 
            request, 
            cancellationToken);
        
        return await result.Content.ReadFromJsonAsync<WarehouseResponse>(cancellationToken: cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid warehouseId, CancellationToken cancellationToken = default)
    => await _client.DeleteAsync($"{BaseUrl}/{warehouseId}", cancellationToken);

}