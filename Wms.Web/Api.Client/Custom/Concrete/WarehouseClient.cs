using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Concrete;

internal sealed class WarehouseClient : IWarehouseClient
{
    private const string ver1 = "/api/v1/";
    
    private readonly HttpClient _client;

    public WarehouseClient(HttpClient client, IOptions<WmsClientOptions> options)
    {
        _client = client;
    }

    public async Task<IReadOnlyCollection<WarehouseResponse>?> GetAllAsync(
        int? offset, int? size, CancellationToken cancellationToken)
    
        => await _client.GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>(
            $"{ver1}warehouses?offset={offset}&size={size}", 
            cancellationToken);

    public async Task<IReadOnlyCollection<WarehouseResponse>?> GetAllDeletedAsync(
        int? offset, int? size, CancellationToken cancellationToken)
    
    => await _client.GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>(
            $"{ver1}warehouses/archive?" +
            $"offset={offset}&size={size}", 
            cancellationToken);

    public async Task<WarehouseResponse?> GetByIdAsync(
        Guid warehouseId,
        int? offset, int? size,
        CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<WarehouseResponse>(
            $"{ver1}warehouses/{warehouseId}?palettesOffset={offset}&palettesSize={size}",
            cancellationToken);

    public async Task<HttpResponseMessage> PostAsync(
        Guid warehouseId, 
        WarehouseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _client.PostAsJsonAsync(
            $"{ver1}warehouses?warehouseId={warehouseId}", 
            request, 
            cancellationToken);
        
        return result;//.Content.ReadFromJsonAsync<WarehouseResponse>(cancellationToken: cancellationToken);
    }
    
    public async Task<HttpResponseMessage> PutAsync(
        Guid warehouseId, 
        WarehouseRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _client.PutAsJsonAsync(
            $"{ver1}warehouses/{warehouseId}", 
            request, 
            cancellationToken);
        
        return result; //.Content.ReadFromJsonAsync<WarehouseResponse>(cancellationToken: cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid warehouseId, CancellationToken cancellationToken)
    => await _client.DeleteAsync($"{ver1}warehouses/{warehouseId}", cancellationToken);

}