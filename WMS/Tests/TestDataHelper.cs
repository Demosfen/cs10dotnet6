using System.Security.Cryptography;
using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

public sealed class TestDataHelper
{
    private static readonly Random Random = new Random();

    private static readonly UnitOfWork UnitOfWork = new();

    /// <summary>
    /// Creates single box with next parameters:
    /// Width/Height/Depth random from 1 to 10
    /// Weight random from 5 to 30
    /// Production/Expiry random difference from 1 to 200 days
    /// </summary>
    /// <param name="paletteId"></param>
    /// <returns></returns>
    public Box CreateBox(Guid paletteId)
        => new Box(paletteId,
            Random.Next(1, 10), Random.Next(1, 10), Random.Next(1, 10),
            Random.Next(5, 30),
            DateTime.Today.AddDays(Random.Next(-100, -1)),
            DateTime.Today.AddDays(Random.Next(0, 100)));

    public List<Box> CreateBoxes(Guid paletteId, int n)
    {
        var boxes = new List<Box>();

        for (int i = 0; i < n; i++) boxes.Add(CreateBox(paletteId));
        
        return boxes;
    }

    public Palette CreatePalette(Guid warehouseId)
        => new Palette(warehouseId,
            Random.Next(20, 30), Random.Next(20, 30), Random.Next(20, 30));
    
    public List<Palette> CreatePalettes(Guid warehouseId, int n)
    {
        var palettes = new List<Palette>();
        
        for (int i = 0; i < n; i++) palettes.Add(CreatePalette(warehouseId));
        
        return palettes;
    }

    public Palette CreatePaletteWithBoxes(Guid warehouseId, int nBoxes)
    {
        var palette = CreatePalette(warehouseId);
        var boxes = CreateBoxes(palette.Id, nBoxes);

        foreach (var box in boxes)
        {
            UnitOfWork.PaletteRepository?.AddBox(palette, box);
        }

        return palette;
    }
    
    public List<Palette> CreatePalettesWithBoxes(Guid warehouseId, int nPalettes, int nBoxes)
    {
        var palettes = CreatePalettes(warehouseId, nPalettes);

        foreach (var palette in palettes)
        {
            var boxes = CreateBoxes(palette.Id, nBoxes);
            
            foreach (var box in boxes)
            {
                UnitOfWork.PaletteRepository?.AddBox(palette, box);
            }
        }
        
        return palettes;
    }
    
    public Warehouse CreateWarehouseWithPalettesAndBoxes(string warehouseName , int nPalettes, int nBoxes)
    {
        var warehouse = new Warehouse(warehouseName);
        
        var palettes = CreatePalettes(warehouse.Id, nPalettes);

        foreach (var palette in palettes)
        {
            var boxes = CreateBoxes(palette.Id, nBoxes);
            
            foreach (var box in boxes)
            {
                UnitOfWork.PaletteRepository?.AddBox(palette, box);
            }
        }
        
        UnitOfWork.WarehouseRepository?.AddPalettes(warehouse, palettes);
        
        return warehouse;
    }
}