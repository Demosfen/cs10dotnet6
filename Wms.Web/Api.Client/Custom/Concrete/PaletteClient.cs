using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Extensions;
using Wms.Web.Api.Contracts.Extensions;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Common.Exceptions;

namespace Wms.Web.Api.Client.Custom.Concrete;

internal sealed class PaletteClient : IPaletteClient
{
    private const string Ver1 = "/api/v1/";
    
    private readonly HttpClient _client;

    public PaletteClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<IReadOnlyCollection<PaletteResponse>?> GetAllAsync(
        Guid warehouseId,
        int? offset, int? size, 
        CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<IReadOnlyCollection<PaletteResponse>>(
            $"{Ver1}warehouses/{warehouseId}/palettes?" +
            $"offset={offset}&size={size}", 
            cancellationToken);

    public async Task<IReadOnlyCollection<PaletteResponse>?> GetAllDeletedAsync(
        Guid warehouseId,
        int? offset, int? size, 
        CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<IReadOnlyCollection<PaletteResponse>>(
            $"{Ver1}warehouses/{warehouseId}/palettes/archive?" +
            $"offset={offset}&size={size}", 
            cancellationToken);

    public async Task<PaletteResponse?> GetByIdAsync(
        Guid paletteId,
        int? offset, int? size,
        CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<PaletteResponse>(
            $"{Ver1}palettes/{paletteId}?boxListOffset={offset}&boxListSize={size}",
            cancellationToken);
    
    public async Task<PaletteResponse?> CreateAsync(
        Guid warehouseId,
        Guid paletteId, 
        PaletteRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _client.PostAsJsonAsync(
            $"{Ver1}warehouses/{warehouseId}?paletteId={paletteId}", 
            request, 
            cancellationToken);

        await result.HandleBadRequestAsync();
        
        switch (result.StatusCode)
        {
            case HttpStatusCode.Conflict:
                throw new EntityAlreadyExistException(paletteId);
            
            case HttpStatusCode.InternalServerError:
                throw new ApiValidationException(result);
        }

        return await result.Content.ReadFromJsonAsync<PaletteResponse>(cancellationToken: cancellationToken);
    }

    public async Task<PaletteResponse?> PutAsync(
        Guid paletteId, 
        Guid warehouseId,
        PaletteRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _client.PutAsJsonAsync(
            $"{Ver1}palettes/{paletteId}?" +
            $"warehouseId={warehouseId}", 
            request, 
            cancellationToken);
        
        await result.HandleBadRequestAsync();
        
        return await result.Content.ReadFromJsonAsync<PaletteResponse>(cancellationToken: cancellationToken);
    }
    
    public async Task<HttpResponseMessage> DeleteAsync(Guid paletteId, CancellationToken cancellationToken)
        => await _client.DeleteAsync($"{Ver1}palettes/{paletteId}", cancellationToken);
}