using WMS.Repositories.Abstract;
using WMS.Store.Entities;

namespace WMS.Repositories.Concrete;

public sealed class WarehouseRepository : IWarehouseRepository
{

    public void AddPalette(Warehouse warehouse, Palette palette)
    {
        if (warehouse.Palettes.Contains(palette))
        {
            Console.WriteLine(
                $"The palette {palette.Id} already added to the warehouse! Skipping...");
                
            return;
        }

        Console.WriteLine($"The palette {palette.Id} added to the warehouse.");

        warehouse.Palettes.Add(palette);
    }

    public void DeletePalette(Warehouse warehouse, Guid paletteId)
    {
        var palette = warehouse.Palettes.SingleOrDefault(x => x.Id == paletteId)
                      ?? throw new InvalidOperationException($"Palette with id = {paletteId} wasn't found");
        
        Console.WriteLine($"Palette with {palette.Id} was removed from the warehouse.");
        
        warehouse.Palettes.Remove(palette);
        
    }
}

