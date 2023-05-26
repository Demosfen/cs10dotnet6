using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Store;
using Wms.Web.Store.Entities;
using Wms.Web.Store.Interfaces;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Extensions;

public sealed class WmsDataHelper: IAsyncLifetime
{
    private readonly IWarehouseDbContext _dbContext;
    
    public WmsDataHelper(IWarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    internal async Task<EntityEntry<Warehouse>> GenerateWarehouse(Guid id)
    {
        return await _dbContext.Warehouses.AddAsync(
            new Warehouse{Id = id, Name = Guid.NewGuid().ToString()}, CancellationToken.None);
    }

    internal async Task<EntityEntry<Palette>> GeneratePalette(
        Guid warehouseId, Guid paletteId, PaletteRequest request)
    {
        return await _dbContext.Palettes.AddAsync(
            new Palette
            {
                Id = paletteId,
                WarehouseId = warehouseId,
                Width = 10,
                Height = 10,
                Depth = 10
            });
    }
/*
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
    }*/
    
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
}