﻿using Wms.Web.Repositories.Concrete;
using Wms.Web.Services.Helpers.Interfaces;
using Wms.Web.Store;
using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Helpers;

public sealed class WarehouseDataGenerator : IWarehouseDataGenerator
{
    private WarehouseDbContext DbContext { get; }

    private readonly BoxRepository _boxRepository;

    private readonly PaletteRepository _paletteRepository;

    private readonly WarehouseRepository _warehouseRepository;

    private static readonly Random Random = new Random();

    public WarehouseDataGenerator(WarehouseDbContext dbContext)
    {
        DbContext = dbContext;
        
        _boxRepository = new BoxRepository(dbContext);

        _paletteRepository = new PaletteRepository(dbContext);

        _warehouseRepository = new WarehouseRepository(dbContext);
    }

    public async Task<Box> CreateBoxAsync(Guid paletteId)
    {
        var box = new Box
        {
            PaletteId = paletteId,
            Weight = Random.Next(1, 10),
            Id = Guid.NewGuid(),
            Width = Random.Next(1, 10),
            Height = Random.Next(1, 10),
            Depth = Random.Next(1, 10),
            ProductionDate = DateTime.Today.AddDays(Random.Next(-100, -1)),
            ExpiryDate = DateTime.Today.AddDays(Random.Next(0, 100))
        };

        // await _paletteRepository.AddBoxAsync(paletteId, box, default);

        await _boxRepository.CreateAsync(box, default);

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
        var palette = new Palette
        {
            WarehouseId = warehouseId,
            Id = Guid.NewGuid(),
            Width = Random.Next(20, 30),
            Height = Random.Next(20, 30),
            Depth = Random.Next(20, 30)
        };
        
        // await _warehouseRepository.AddPaletteAsync(warehouseId, palette, default);

        await _paletteRepository.CreateAsync(palette, default);
        
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
        var warehouse = new Warehouse
        {
            Id = Guid.NewGuid(),
            Name = warehouseName
        };

        await _warehouseRepository.CreateAsync(warehouse, default);

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