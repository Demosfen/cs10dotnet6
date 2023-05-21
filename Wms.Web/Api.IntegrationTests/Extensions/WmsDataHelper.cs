using Wms.Web.Api.Contracts.Requests;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Extensions;

public sealed class WmsDataHelper: IAsyncLifetime
{
    private readonly HttpClient _httpClient;
    private const string Ver1 = "/api/v1/";
    
    public WmsDataHelper(TestApplication apiFactory)
    {
        _httpClient = apiFactory.HttpClient;
    }

    internal async Task<HttpResponseMessage> GenerateWarehouse(Guid id)
    {
        return await _httpClient.PostAsJsonAsync(
            $"{Ver1}warehouses?warehouseId={id}", 
            new WarehouseRequest{ Name = $"{id}" }, CancellationToken.None);
    }

    internal async Task<HttpResponseMessage> GeneratePalette(
        Guid warehouseId, Guid paletteId, PaletteRequest request)
    {
        return await _httpClient.PostAsJsonAsync(
            $"{Ver1}warehouses/{warehouseId}?paletteId={paletteId}", request, CancellationToken.None); 
    }
    
    internal async Task<HttpResponseMessage> GenerateBox(
        Guid paletteId, Guid boxId, BoxRequest request)
    {
        return await _httpClient.PostAsJsonAsync(
            $"{Ver1}palettes/{paletteId}?boxId={boxId}", request, CancellationToken.None);
    }
    
    internal async Task<HttpResponseMessage> DeleteWarehouse(Guid id)
    {
        return await _httpClient.DeleteAsync($"{Ver1}warehouses/{id}", CancellationToken.None);
    }

    internal async Task<HttpResponseMessage> DeletePalette(Guid id)
    {
        return await _httpClient.DeleteAsync($"{Ver1}palettes/{id}", CancellationToken.None);
    }
    
    internal async Task<HttpResponseMessage> DeleteBox(Guid id)
    {
        return await _httpClient.DeleteAsync($"{Ver1}boxes/{id}", CancellationToken.None);
    }
    
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
}