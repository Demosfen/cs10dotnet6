using System.Net.Http.Json;
using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Console.Clients;

public class WmsClient : IWmsClient
{
    private readonly HttpClient _client;

    public WmsClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<WarehouseRequest?> PostAsync(Guid warehouseId, string name, CancellationToken cancellationToken)
    {
        var result = await _client.PostAsJsonAsync(
            $"/api/v1/warehouses?warehouseId={warehouseId}", 
            new WarehouseRequest{ Name = name }, cancellationToken);

        
        return await result.Content.ReadFromJsonAsync<WarehouseRequest>(cancellationToken: cancellationToken);
    }
}