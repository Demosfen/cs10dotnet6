using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Common.Exceptions;
using Wms.Web.Store;
using Wms.Web.Store.Entities;
using Wms.Web.Store.Interfaces;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Extensions;

public sealed class DataHelper: IAsyncLifetime
{
    private readonly IWarehouseDbContext _dbContext;
    
    public DataHelper(IWarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    internal async Task GenerateWarehouse(Guid id)
    {
        await _dbContext.Warehouses.AddAsync(
            new Warehouse{Id = id, Name = Guid.NewGuid().ToString()}, CancellationToken.None);

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    internal async Task GeneratePalette(Guid warehouseId, Guid paletteId)
    {
        await _dbContext.Palettes.AddAsync(
            new Palette
            {
                Id = paletteId,
                WarehouseId = warehouseId,
                Width = 10,
                Height = 10,
                Depth = 10
            });

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    internal async Task GenerateBox(
        Guid paletteId, Guid boxId, BoxRequest request)
    {
        await _dbContext.Boxes.AddAsync(
            new Box
            {
                Id = boxId,
                PaletteId = paletteId,
                Width = 5,
                Height = 5,
                Depth = 5,
                Weight = 5,
                ProductionDate = new DateTime(2007,1,1),
                ExpiryDate = new DateTime(2008,1,1)
            });
        
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    internal async Task DeleteWarehouse(Guid id)
    { 
        var entity = await _dbContext.Warehouses.FindAsync(id)
                         ?? throw new EntityNotFoundException(id);

        _dbContext.Warehouses.Remove(entity);

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    internal async Task DeletePalette(Guid id)
    {
        var entity = await _dbContext.Palettes.FindAsync(id)
            ?? throw new EntityNotFoundException(id);

        _dbContext.Palettes.Remove(entity);

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    internal async Task DeleteBox(Guid id)
    {
        var entity = await _dbContext.Boxes.FindAsync(id)
            ?? throw new EntityNotFoundException(id);

        _dbContext.Boxes.Remove(entity);

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
    
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
}