using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using WMS.Store.Entities;

namespace WMS.Store.Helpers;

public abstract class WarehouseDataGenerator
{
    protected readonly WarehouseDbContext _dbContext;
    
    private static readonly Random Random = new Random();

    protected WarehouseDataGenerator(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    internal async Task<Box> CreateBoxAsync(Guid paletteId)
    {
        var box = new Box(paletteId,
            Random.Next(1, 10), Random.Next(1, 10), Random.Next(1, 10),
            Random.Next(5, 30),
            DateTime.Today.AddDays(Random.Next(-100, -1)),
            DateTime.Today.AddDays(Random.Next(0, 100)));

        await _dbContext.Boxes.AddAsync(box);
        await _dbContext.SaveChangesAsync();

        return box;
    }
    
    internal async Task<List<Box>> CreateBoxesAsync(Guid paletteId, int n)
    {
        var boxes = new List<Box>();

        for (int i = 0; i < n; i++)
        {
            boxes.Add(await CreateBoxAsync(paletteId));
        }
        
        return boxes;
    }

    internal async Task<Palette> CreatePaletteAsync(Guid warehouseId)
    {
        var palette = new Palette(warehouseId,
            Random.Next(20, 30), Random.Next(20, 30), Random.Next(20, 30));

        _dbContext.Palettes.Add(palette);
        await _dbContext.SaveChangesAsync();
        return palette;
    }
    
    internal async Task<List<Palette>> CreatePalettesAsync(Guid warehouseId, int n)
    {
        var palettes = new List<Palette>();

        for (int i = 0; i < n; i++)
        {
            palettes.Add(await CreatePaletteAsync(warehouseId));
        }
        
        return palettes;
    }

    internal async Task<Warehouse> CreateWarehouse(string warehouseName)
    {
        var warehouse = _dbContext.Warehouses.Add(new Warehouse(warehouseName));

        await _dbContext.SaveChangesAsync();

        return warehouse.Entity;
    }

    internal async Task<Palette> CreatePaletteWithBoxesAsync(Guid warehouseId, int nBoxes)
    {
        var palette = await CreatePaletteAsync(warehouseId);
        await CreateBoxesAsync(palette.Id, nBoxes);

        return palette;
    }

    internal async Task<List<Palette>> CreatePalettesWithBoxesAsync(Guid warehouseId, int nPalettes, int nBoxes)
    {
        var palettes = await CreatePalettesAsync(warehouseId, nPalettes);

        foreach (var palette in palettes)
        {
            await CreateBoxesAsync(palette.Id, nBoxes);
        }

        return palettes;
    }

    internal async Task<Warehouse> CreateWarehouseWithPalettesAndBoxes(string warehouseName , int nPalettes, int nBoxes)
    {
        var warehouse = await CreateWarehouse(warehouseName);
        
        await CreatePalettesWithBoxesAsync(warehouse.Id, nPalettes, nBoxes);

        return warehouse;
    }
}