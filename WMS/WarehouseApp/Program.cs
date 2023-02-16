using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext.Entities;
using WMS.Services.Concrete;
using static System.Console;

namespace WMS.WarehouseApp;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        await using var context = new WarehouseDbContext.WarehouseDbContext();
        // await context.Database.EnsureDeletedAsync(); // Removes DB file
        // await context.Database.EnsureCreatedAsync(); // Creates DB file
        await context.Database.MigrateAsync();

        var warehouseRepository = new WarehouseRepository(context);
        var paletteRepository = new PaletteRepository(context);
        // var boxRepository = new BoxRepository(context);

        var queryService = new WarehouseQueryService(context);
        
        var warehouse = new Warehouse("Test warehouse");
        await warehouseRepository.CreateAsync(warehouse);
        
        var palette1 = new Palette(warehouse.Id, 1, 1, 1);
        var palette2 = new Palette(warehouse.Id,3, 3, 3);
        var palette3 = new Palette(warehouse.Id,10, 10, 10);
        var box1 = new Box(palette1.Id, 1, 1, 1, 6, new DateTime(2007, 1, 1));
        
        paletteRepository.AddBox(palette1,box1);
        
        warehouseRepository.AddPalette(warehouse, palette1);
        
        await paletteRepository.CreateAsync(palette1);
        await paletteRepository.CreateAsync(palette2);
        await paletteRepository.CreateAsync(palette3);

        await warehouseRepository.UpdateAsync(warehouse);

        // Grouped by Expiry date and ordered by Weight
        var groupedPalettes = queryService.SortByExpiryAndWeightAlternative(warehouse.Id);

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
        var threePalettes = queryService.ChooseThreePalettesByExpiryAndVolumeAlternative(warehouse.Id);

        foreach (var palette in threePalettes)
        {
            WriteLine(palette);
        }
    }
}