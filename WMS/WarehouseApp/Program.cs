using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Concrete;
using WMS.Services.Concrete;
using WMS.Services.Helpers;
using WMS.Store.Entities;
using static System.Console;

namespace WMS.WarehouseApp;

public static class Program
{
    public static async Task Main()
    {
        await using var context = new Store.WarehouseDbContext();

        await context.Database.MigrateAsync();

        var queryService = new WarehouseQueryService(context);

        var dataGenerator = new WarehouseDataGenerator(context); //TODO: Почему я не могу сделать WarehouseDataGeтerator internal и он не виден здесь?

        var warehouse = await dataGenerator.CreateWarehouseWithPalettesAndBoxes("Production warehouse", 5, 7);

        // Grouped by Expiry date and ordered by Weight
        var groupedPalettes = await queryService.SortByExpiryAndWeightAsync(warehouse.Id, default);

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
        var threePalettes = await queryService.ChooseThreePalettesByExpiryAndVolumeAsync(warehouse.Id, default);

        foreach (var palette in threePalettes)
        {
            WriteLine(palette);
        }
    }
}