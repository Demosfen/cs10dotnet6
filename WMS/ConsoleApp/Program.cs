using WMS.Data;
using WMS.Services.Concrete;

using static System.Console;

namespace WMS.ConsoleApp;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // create a warehouse
        Warehouse warehouse = new();

        var smallPaletteOptional1 = new Palette(2, 2, 2);

        var smallPaletteOptional2 = new Palette(2, 2, 2);
            
        
        warehouse.AddPalette(smallPaletteOptional1);
        warehouse.AddPalette(smallPaletteOptional2);
        
        smallPaletteOptional1.AddBox(new Box(0.1m,0.1m,0.1m,10,new DateTime(2010,01,01)));
        smallPaletteOptional2.AddBox(new Box(0.2m,0.3m,0.4m,12, new DateTime(2011,1,1)));

        // create warehouse repo, serializing/deserializing warehouse, save and load: ok!
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
        }
    }
}