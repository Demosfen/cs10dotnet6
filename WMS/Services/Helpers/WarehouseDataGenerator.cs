using WMS.Store;
using WMS.Store.Entities;
using WMS.Store.Interfaces;
using WMS.Repositories.Concrete;

namespace WMS.Services.Helpers;

public sealed class WarehouseDataGenerator : IWarehouseDataGenerator
{
    private WarehouseDbContext DbContext { get; }

    private readonly BoxRepository _boxRepository;

    private readonly PaletteRepository _paletteRepository;

    private readonly WarehouseRepository _warehouseRepository;

    private static readonly Random Random = new Random();

    public WarehouseDataGenerator(WarehouseDbContext dbContext) //TODO: Вопрос по модификаторам доступа. 
    {
        DbContext = dbContext;
        
        _boxRepository = new BoxRepository(dbContext);

        _paletteRepository = new PaletteRepository(dbContext);

        _warehouseRepository = new WarehouseRepository(dbContext);
    }

    public async Task<Box> CreateBoxAsync(Guid paletteId)
    {
        var box = new Box(paletteId,
            Random.Next(1, 10), Random.Next(1, 10), Random.Next(1, 10),
            Random.Next(5, 30),
            DateTime.Today.AddDays(Random.Next(-100, -1)),
            DateTime.Today.AddDays(Random.Next(0, 100)));
        
        await _paletteRepository.AddBoxAsync(paletteId, box, default);

        await _boxRepository.AddAsync(box, default);

        var palette = await _paletteRepository.GetByIdAsync(paletteId, default);

        await DbContext.SaveChangesAsync();

        return box;
    }

    public async Task<List<Box>> CreateBoxesAsync(Guid paletteId, int n)
    {
        var boxes = new List<Box>();

        for (int i = 0; i < n; i++)
        {
            boxes.Add(await CreateBoxAsync(paletteId));
        }
        
        return boxes;
    }

    public async Task<Palette> CreatePaletteAsync(Guid warehouseId)
    {
        var palette = new Palette(warehouseId,
            Random.Next(20, 30), Random.Next(20, 30), Random.Next(20, 30));
        
        await _warehouseRepository.AddPaletteAsync(warehouseId, palette, default);

        await _paletteRepository.AddAsync(palette, default);
        
        await DbContext.SaveChangesAsync();
        
        return palette;
    }

    public async Task<List<Palette>> CreatePalettesAsync(Guid warehouseId, int n)
    {
        var palettes = new List<Palette>();

        for (int i = 0; i < n; i++)
        {
            palettes.Add(await CreatePaletteAsync(warehouseId));
        }
        
        return palettes;
    }

    public async Task<Warehouse> CreateWarehouse(string warehouseName)
    {
        var warehouse = new Warehouse(warehouseName);

        await _warehouseRepository.AddAsync(warehouse, default);

        await DbContext.SaveChangesAsync();

        return warehouse;
    }

    public async Task<Palette> CreatePaletteWithBoxesAsync(Guid warehouseId, int nBoxes)
    {
        var palette = await CreatePaletteAsync(warehouseId);
        await CreateBoxesAsync(palette.Id, nBoxes);

        return palette;
    }

    public async Task<List<Palette>> CreatePalettesWithBoxesAsync(Guid warehouseId, int nPalettes, int nBoxes)
    {
        var palettes = await CreatePalettesAsync(warehouseId, nPalettes);

        foreach (var palette in palettes)
        {
            await CreateBoxesAsync(palette.Id, nBoxes);
        }

        return palettes;
    }

    public async Task<Warehouse> CreateWarehouseWithPalettesAndBoxes(string warehouseName , int nPalettes, int nBoxes)
    {
        var warehouse = await CreateWarehouse(warehouseName);
        
        await CreatePalettesWithBoxesAsync(warehouse.Id, nPalettes, nBoxes);

        return warehouse;
    }
}