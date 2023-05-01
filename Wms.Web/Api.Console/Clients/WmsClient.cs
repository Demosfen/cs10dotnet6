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

    public async Task<WarehouseRequest?> PostAsync(Guid id, CancellationToken cancellationToken)
    {
        var warehouseId = Guid.NewGuid();

        var result = await _client.PostAsJsonAsync(
            $"/api/v1/warehouses?warehouseId={warehouseId}", 
            new WarehouseRequest{ Name = "TestClient2" }, cancellationToken);

        
        return await result.Content.ReadFromJsonAsync<WarehouseRequest>(cancellationToken: cancellationToken);
    }
}