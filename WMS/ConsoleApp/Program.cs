using WMS.Store;
using WMS.Store.Entities;
using Microsoft.EntityFrameworkCore;
using WMS.Services.Concrete;

namespace WMS.ConsoleApp;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // create a warehouse
        Warehouse warehouse = new();
        
        WarehouseRepository warehouseRepository = new();

        PaletteRepository paletteRepository = new();

        var Palette1 = new Palette(1, 1, 1);

        var Palette2 = new Palette(2, 2, 2);
            
        
        warehouseRepository.AddPalette(warehouse, Palette1);
        warehouseRepository.AddPalette(warehouse, Palette2);
        
        paletteRepository.AddBox(Palette1, new Box(0.1m,0.1m,0.1m,10,new DateTime(2010,01,01)));
        paletteRepository.AddBox(Palette2, new Box(0.2m,0.3m,0.4m,12, new DateTime(2011,1,1)));
        
        await using var context = new WarehouseContext();
        await context.Database.MigrateAsync();

        context.Warehouses.Add(warehouse);
        await context.SaveChangesAsync();
        Console.WriteLine("Success!");

        /*// create warehouse repo, serializing/deserializing warehouse, save and load: ok!
        WarehouseRepository repository = new();
        
        await repository.Save(warehouse, "warehouse.json").ConfigureAwait(false);

        var loadedWarehouse = await repository.Read("warehouse.json").ConfigureAwait(false);
        
        //WriteLine(loadedWarehouse);

        WareHouseQueryService service = new();

        var sortedPalettes = service.SortByExpiryAndWeight(warehouse);

        WriteLine("\nSorted by ExpiryDate and Weight palettes:\n");

        foreach (var palette in sortedPalettes)
        {
            WriteLine(palette);
        }
        
        WriteLine("\nThree palettes with max Expiry SortedBy Volume:\n");

        var threePalettes = service.ChooseThreePalettesByExpiryAndVolume(warehouse);

        foreach (var palette in threePalettes)
        {
            WriteLine(palette);
        }*/
    }
}