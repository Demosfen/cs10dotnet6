﻿using WMS.Data;
using WMS.Services.Concrete;
using static System.Console;

namespace WMS.ConsoleApp;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // create a warehouse
        Warehouse warehouse = new();
        
        // create a palette
        Palette palette1 = new (
            10,10,10);

        //create two boxes
        Box box1 = new(
            10, 1, 10, 5,
            new DateTime(2008,1,1));
        Box box2 = new(
            1, 1, 1, 3,
            new DateTime(2009,1,1));

        // add boxes to the palette: ok!
        palette1.AddBox(box1);
        palette1.AddBox(box2);
        
        // add box1 second time and checking the verification: ok!
        palette1.AddBox(box1);
        
        // checking the deletion of the box: ok!
        palette1.DeleteBox(box1);
        
        // return the box on the palette
        palette1.AddBox(box1);
        
        // put the palette into the warehouse: ok!
        warehouse.AddPalette(palette1);
        
        // verify if put palette twice: ok!
        warehouse.AddPalette(palette1);

        WriteLine(warehouse);
        
        //warehouse.Palettes.Add(palette1); // check readonly properties: ok!

        // create warehouse repo, serializing/deserializing warehouse, save and load: ok!
        WarehouseRepository repository = new();
        
        await repository.Save(warehouse, "warehouse.json").ConfigureAwait(false);

        var loadedWarehouse = await repository.Read("warehouse.json").ConfigureAwait(false);
        
        WriteLine(loadedWarehouse);
        
        // palette deletion from the initial warehouse: ok!
        warehouse.DeletePalette(palette1);
        
        WriteLine(warehouse);
        
        // palette deletion from the deserialized warehouse: bug!
        loadedWarehouse.DeletePalette(palette1);

        WriteLine(loadedWarehouse); //TODO fix bug with deletion of the palette from deserialized object
    }
}