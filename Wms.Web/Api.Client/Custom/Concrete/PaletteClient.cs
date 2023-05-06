using System.Net.Http.Json;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Concrete;

public class PaletteClient : IPaletteClient
{
    private readonly HttpClient _client;
    private const string BaseUrl = "/api/v1/";

    public PaletteClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<IReadOnlyCollection<PaletteResponse>?> GetAllAsync(
        Guid warehouseId,
        int? offset, int? size, 
        CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<IReadOnlyCollection<PaletteResponse>>(
            $"{BaseUrl}warehouses/{warehouseId}/palettes?offset={offset}&limit={size}", 
            cancellationToken);

    // public async Task<IReadOnlyCollection<PaletteResponse>?> GetAllDeletedAsync(int? offset, int? size, CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task<WarehouseResponse?> PostAsync(Guid paletteId, PaletteRequest request, CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task<WarehouseResponse?> PutAsync(Guid paletteId, PaletteRequest request, CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task<HttpResponseMessage> DeleteAsync(Guid paletteId, CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }
}