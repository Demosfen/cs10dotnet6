using WMS.Store.Entities;
using WMS.Tests.Infrastructure;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests.Abstract;

[Collection(DbTestCollection.Name)]
public abstract class WarehouseTestsBase 
    : IClassFixture<TestDatabaseFixture>, IAsyncLifetime
{
    protected readonly Store.WarehouseDbContext DbContext;
    
    private static readonly Random Random = new Random();

    protected WarehouseTestsBase(TestDatabaseFixture fixture)
    {
        DbContext = fixture.CreateDbContext();
    }

    internal async Task<Box> CreateBoxAsync(Guid paletteId)
    {
        var box = new Box(paletteId,
            Random.Next(1, 10), Random.Next(1, 10), Random.Next(1, 10),
            Random.Next(5, 30),
            DateTime.Today.AddDays(Random.Next(-100, -1)),
            DateTime.Today.AddDays(Random.Next(0, 100)));
        DbContext.Boxes.Add(box);

        await DbContext.SaveChangesAsync();

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

        DbContext.Palettes.Add(palette);
        await DbContext.SaveChangesAsync();
        return palette;
    }
    
    internal async Task<List<Palette>> CreatePalettesAsync(Guid warehouseId, int n)
    {
        var palettes = new List<Palette>();

        for (int i = 0; i < n; i++)
        {
            palettes.Add(await CreatePaletteAsync(warehouseId));
        }
        
        await DbContext.SaveChangesAsync();
        
        return palettes;
    }

    internal async Task<Warehouse> CreateWarehouse(string warehouseName)
    {
        var warehouse = DbContext.Warehouses.Add(new Warehouse(warehouseName));

        await DbContext.SaveChangesAsync();

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

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await DbContext.DisposeAsync();
}