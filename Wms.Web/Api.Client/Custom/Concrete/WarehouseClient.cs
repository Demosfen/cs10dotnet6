using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Extensions;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Common.Exceptions;

namespace Wms.Web.Api.Client.Custom.Concrete;

internal sealed class WarehouseClient : IWarehouseClient
{
    private const string Ver1 = "/api/v1/";
    
    private readonly HttpClient _client;

    public WarehouseClient(HttpClient client, IOptions<WmsClientOptions> options)
    {
        _client = client;
    }

    public async Task<IReadOnlyCollection<WarehouseResponse>?> GetAllAsync(
        int? offset, int? size, CancellationToken cancellationToken)
    
        => await _client.GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>(
            $"{Ver1}warehouses?offset={offset}&size={size}", 
            cancellationToken);

    public async Task<IReadOnlyCollection<WarehouseResponse>?> GetAllDeletedAsync(
        int? offset, int? size, CancellationToken cancellationToken)
    
    => await _client.GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>(
            $"{Ver1}warehouses/archive?" +
            $"offset={offset}&size={size}", 
            cancellationToken);

    public async Task<WarehouseResponse?> GetByIdAsync(
        Guid warehouseId,
        int? offset, int? size,
        CancellationToken cancellationToken)
        => await _client.GetFromJsonAsync<WarehouseResponse>(
            $"{Ver1}warehouses/{warehouseId}?palettesOffset={offset}&palettesSize={size}",
            cancellationToken);

    public async Task<WarehouseResponse?> CreateAsync(
        Guid warehouseId, 
        WarehouseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _client.PostAsJsonAsync(
            $"{Ver1}warehouses?warehouseId={warehouseId}", 
            request, cancellationToken);

        if (result.StatusCode == HttpStatusCode.Conflict)
        {
            throw new EntityAlreadyExistException(warehouseId);
        }

        if (result.StatusCode == HttpStatusCode.BadRequest)
        {
            var content = await result.Content.ReadAsStringAsync(cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<WmsProblemDetails>(content);

                problemDetails!.Instance = result.RequestMessage?.RequestUri?.ToString();
                problemDetails.Detail = "See the errors property for details.";
                
                throw new ApiValidationException("API request failed!", problemDetails);
            }
        }
        else if (result.StatusCode == HttpStatusCode.InternalServerError)
        {
            var problemDetails = await result.Content
                .ReadAsStringAsync(cancellationToken: cancellationToken);
        }

        return await result.Content.ReadFromJsonAsync<WarehouseResponse>(cancellationToken: cancellationToken);
    }
    
    public async Task<HttpResponseMessage> PutAsync(
        Guid warehouseId, 
        WarehouseRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _client.PutAsJsonAsync(
            $"{Ver1}warehouses/{warehouseId}", 
            request, 
            cancellationToken);
        
        return result;
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid warehouseId, CancellationToken cancellationToken)
    => await _client.DeleteAsync($"{Ver1}warehouses/{warehouseId}", cancellationToken);

}