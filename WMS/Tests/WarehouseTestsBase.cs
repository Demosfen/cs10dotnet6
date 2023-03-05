using Microsoft.EntityFrameworkCore.Design;
using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

[Collection(DbTestCollection.Name)]
public abstract class WarehouseTestsBase : IClassFixture<TestDatabaseFixture>, IAsyncLifetime
{
    protected readonly Store.WarehouseDbContext DbContext;

    protected readonly UnitOfWork UnitOfWork;
    
    private static readonly Random Random = new Random();

    protected WarehouseTestsBase(TestDatabaseFixture fixture)
    {
        DbContext = fixture.CreateDbContext();
        UnitOfWork = new UnitOfWork(DbContext);
    }

    internal async Task<Box> CreateBoxAsync(Guid paletteId)
    {
        var box = new Box(paletteId,
            Random.Next(1, 10), Random.Next(1, 10), Random.Next(1, 10),
            Random.Next(5, 30),
            DateTime.Today.AddDays(Random.Next(-100, -1)),
            DateTime.Today.AddDays(Random.Next(0, 100)));

        await UnitOfWork.BoxRepository.InsertAsync(box);

        await UnitOfWork.SaveAsync();
        
        return box;
    }

    internal async Task<List<Box>> CreateBoxesAsync(Guid paletteId, int n)
    {
        var boxes = new List<Box>();

        for (int i = 0; i < n; i++)
        {
            boxes.Add(await CreateBoxAsync(paletteId));

            await UnitOfWork.BoxRepository.InsertAsync(boxes[i]);
        }

        await UnitOfWork.SaveAsync();
        
        return boxes;
    }

    internal async Task<Palette> CreatePaletteAsync(Guid warehouseId)
    {
        var palette = new Palette(warehouseId,
            Random.Next(20, 30), Random.Next(20, 30), Random.Next(20, 30));

        await UnitOfWork.PaletteRepository.InsertAsync(palette);

        await UnitOfWork.SaveAsync();

        return palette;
    }
        

    internal async Task<List<Palette>> CreatePalettesAsync(Guid warehouseId, int n)
    {
        var palettes = new List<Palette>();

        for (int i = 0; i < n; i++)
        {
            palettes.Add(await CreatePaletteAsync(warehouseId));

            await UnitOfWork.PaletteRepository.InsertAsync(palettes[i]);
        }

        await UnitOfWork.SaveAsync();
        
        return palettes;
    }

    internal async Task<Palette> CreatePaletteWithBoxesAsync(Guid warehouseId, int nBoxes)
    {
        var palette = await CreatePaletteAsync(warehouseId);
        var boxes = await CreateBoxesAsync(palette.Id, nBoxes);

        foreach (var box in boxes)
        {
            UnitOfWork.PaletteRepository.AddBox(palette, box);

            await UnitOfWork.BoxRepository.InsertAsync(box);
        }

        await UnitOfWork.SaveAsync();

        return palette;
    }

    internal async Task<List<Palette>> CreatePalettesWithBoxesAsync(Guid warehouseId, int nPalettes, int nBoxes)
    {
        var palettes = await CreatePalettesAsync(warehouseId, nPalettes);

        foreach (var palette in palettes)
        {
            var boxes = await CreateBoxesAsync(palette.Id, nBoxes);
            
            foreach (var box in boxes)
            {
                UnitOfWork.PaletteRepository.AddBox(palette, box);

                await UnitOfWork.BoxRepository.InsertAsync(box);
            }

            await UnitOfWork.PaletteRepository.InsertAsync(palette);
        }

        await UnitOfWork.SaveAsync();
        
        return palettes;
    }

    internal async Task<Warehouse> CreateWarehouseWithPalettesAndBoxes(string warehouseName , int nPalettes, int nBoxes)
    {
        var warehouse = new Warehouse(warehouseName);
        
        var palettes = await CreatePalettesWithBoxesAsync(warehouse.Id, nPalettes, nBoxes);

        UnitOfWork.WarehouseRepository.AddPalettes(warehouse, palettes);

        await UnitOfWork.WarehouseRepository.InsertAsync(warehouse);

        await UnitOfWork.SaveAsync();

        return warehouse;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await DbContext.DisposeAsync();
}