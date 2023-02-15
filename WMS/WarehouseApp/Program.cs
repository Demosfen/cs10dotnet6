using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext.Entities;

using static System.Console;

namespace WMS.WarehouseApp;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        await using var context = new WarehouseDbContext.WarehouseDbContext();
        //await context.Database.EnsureDeletedAsync();
        //await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();

        var warehouseRepository = new WarehouseRepository(context);
        var paletteRepository = new PaletteRepository(context);
        var boxRepository = new BoxRepository(context);
        // await warehouseRepository.DeleteAllAsync(); TODO SaveAsync() exception --> SQLite Error 19: 'FOREIGN KEY constraint failed'

        
        var warehouse = new Warehouse();
        var palette1 = new Palette(1, 1, 1);
        var palette2 = new Palette(3, 3, 3);
        var palette3 = new Palette(10, 10, 10);
        var box1 = new Box(1, 1, 1, 6, new DateTime(2007, 1, 1));
        var box2 = new Box(2, 2, 2, 5, new DateTime(2008, 1, 1));
        var box3 = new Box(3, 3, 3, 4, new DateTime(2009, 1, 1));
        var box4 = new Box(4, 4, 4, 3, new DateTime(2010, 1, 1));
        var box5 = new Box(5, 5, 5, 2, new DateTime(2011, 1, 1));
        var box6 = new Box(6, 6, 6, 1, new DateTime(2012, 1, 1));
        
        await warehouseRepository.CreateAsync(warehouse);
        await paletteRepository.CreateAsync(palette1);
        await paletteRepository.CreateAsync(palette2);
        await paletteRepository.CreateAsync(palette3);
        await boxRepository.CreateAsync(box1);
        await boxRepository.CreateAsync(box2);
        await boxRepository.CreateAsync(box3);
        await boxRepository.CreateAsync(box4);
        await boxRepository.CreateAsync(box5);
        await boxRepository.CreateAsync(box6);

        warehouseRepository.AddPalette(warehouse, palette1);
        warehouseRepository.AddPalette(warehouse,palette2);
        warehouseRepository.AddPalette(warehouse, palette3);
        
        paletteRepository.AddBox(palette1,box1);
        paletteRepository.AddBox(palette2,box2);
        paletteRepository.AddBox(palette2,box3);
        paletteRepository.AddBox(palette3,box4);
        paletteRepository.AddBox(palette3,box5);
        paletteRepository.AddBox(palette3,box6);
        
        await warehouseRepository.UpdateAsync(warehouse);

        // Grouped by Expiry date and ordered by Weight
        var groupedPalettes = context.Palettes
            .Where(p => p.ExpiryDate.HasValue)
            .OrderBy(p => p.ExpiryDate)
            .ThenBy(p => p.Weight)
            .GroupBy(g => g.ExpiryDate).ToList();

        foreach (var paletteGroup in groupedPalettes)
        {
            WriteLine($"Palette {paletteGroup.Key}");
            
            foreach (var palette in paletteGroup)
            {
                WriteLine(palette);

                foreach (var box in palette.Boxes)
                {
                    WriteLine(box);
                }
            }
        }
        
        // Select three palettes with Expiry and ordered by Volume
        var threePalettes = context.Palettes
            .OrderBy(p => p.ExpiryDate)
            .ThenBy(p => p.Volume)
            .Take(3).ToList();

        foreach (var palette in threePalettes)
        {
            WriteLine(palette);
        }
    }
}