using System.Net.Http.Json;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Console.Clients;

public class WmsClient : IWmsClient
{
    private readonly HttpClient _client;

    public WmsClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<WarehouseResponse?> PostAsync(
        Guid id, CancellationToken cancellationToken)
    {
        var warehouseId = Guid.NewGuid();

        var result = await _client.PostAsJsonAsync(
            $"/api/v1/warehouses/{warehouseId}", 
            new WarehouseResponse{Id = warehouseId, Name = "TestClient1" }, cancellationToken);

        return await result.Content.ReadFromJsonAsync<WarehouseResponse>(cancellationToken: cancellationToken);
    }
}