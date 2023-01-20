using WMS.Data;
using WMS.Services.Concrete;
using WMS.Services.Extensions;
using static System.Console;

namespace WMS.ConsoleApp;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Palette palette1 = new (
            10,10,10);

        Box box1 = new(
            10, 1, 10, 5,
            new DateTime(2008,1,1));
        Box box2 = new(
            1, 1, 1, 3,
            new DateTime(2009,1,1));

        palette1.AddBox(box1);
        palette1.AddBox(box2);
        palette1.AddBox(box1);
        palette1.DeleteBox(box1);
        palette1.AddBox(box1);
        
        Warehouse warehouse = new();
        
        warehouse.AddPalette(palette1);
        warehouse.AddPalette(palette1);

        WriteLine(warehouse);
        
        //warehouse.Palettes.Add(palette1); //not allowed!

        WarehouseRepository repository = new();
        await repository.Save(warehouse, "warehouse.json").ConfigureAwait(false);

        Warehouse loadedWarehouse = await repository.Read("warehouse.json").ConfigureAwait(false);
        
        WriteLine(loadedWarehouse);
        
        loadedWarehouse.DeletePalette(palette1);
        
        WriteLine(loadedWarehouse);
    }
}