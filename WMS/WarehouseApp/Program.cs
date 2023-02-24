using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext.Entities;
using WMS.Services.Concrete;
using static System.Console;

namespace WMS.WarehouseApp;

internal static class Program
{
    public static async Task Main()
    {
        await using var context = new WarehouseDbContext.WarehouseDbContext();

        await context.Database.MigrateAsync();

        var unitOfWork = new UnitOfWork();

        var queryService = new WarehouseQueryService(context);
        
        var warehouse = new Warehouse("Warehouse #2");
        
        unitOfWork.WarehouseRepository?.InsertAsync(warehouse);

        var palette1 = new Palette(warehouse.Id, 1, 1, 1);
        var palette2 = new Palette(warehouse.Id,3, 3, 3);
        var palette3 = new Palette(warehouse.Id,10, 10, 10);
        var box1 = new Box(palette1.Id, 1, 1, 1, 6, new DateTime(2007, 1, 1));
        
        unitOfWork.PaletteRepository?.AddBox(palette1,box1);
        
        unitOfWork.WarehouseRepository?.AddPalette(warehouse, palette1);
        unitOfWork.WarehouseRepository?.AddPalette(warehouse, palette2);
        unitOfWork.WarehouseRepository?.AddPalette(warehouse, palette3);
        
        await unitOfWork.SaveAsync();

        // Grouped by Expiry date and ordered by Weight
        var groupedPalettes = queryService.SortByExpiryAndWeight(warehouse.Id);

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
        var threePalettes = queryService.ChooseThreePalettesByExpiryAndVolume(warehouse.Id);

        foreach (var palette in threePalettes)
        {
            WriteLine(palette);
        }
        
        // unitOfWork.WarehouseRepository?.DeleteAsync(new Guid("2B3E0CD8-9474-447D-B589-59467727F111"));
        // await unitOfWork.Save();

        // var warehouses = unitOfWork.WarehouseRepository?.GetAllAsync(includeProperties: "Palettes");
        //
        // Console.WriteLine(warehouse);
    }
}