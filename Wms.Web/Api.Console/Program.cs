using System.Net.Http.Json;
using Wms.Web.Api.Contracts.Responses;

using var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5003")
};

var warehouseId = Guid.NewGuid();

var result = await httpClient.PostAsJsonAsync<WarehouseResponse>(
    $"/api/v1/warehouses/{warehouseId}", 
    new WarehouseResponse{Id = warehouseId, Name = "TestClient" });
        
Console.ReadKey();