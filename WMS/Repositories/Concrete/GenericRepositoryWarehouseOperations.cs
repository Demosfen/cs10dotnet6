using WMS.WarehouseDbContext.Entities;
using WMS.WarehouseDbContext.Interfaces;

namespace WMS.Repositories.Concrete;

public partial class GenericRepository<TEntity> 
    where TEntity : class, IEntityWithId, ISoftDeletable
{
    /// <summary>
    /// Put palette to the warehouse
    /// </summary>
    /// <param name="warehouse">Where to put palette</param>
    /// <param name="palette">What palette to put</param>
    public void AddPalette(Warehouse warehouse, Palette palette)
    {
        if (warehouse.Palettes.Contains(palette))
        {
            Console.WriteLine(
                $"Palette {palette.Id} already added to the warehouse {warehouse.Id}! Skipping...");

            return;
        }

        Console.WriteLine($"Palette {palette.Id} added to the warehouse {warehouse.Id}.");

        warehouse.Palettes.Add(palette);
        palette.WarehouseId = warehouse.Id;
    }
    
    public void AddPalettes(Warehouse warehouse, List<Palette> palettes)
    {
        foreach (var palette in palettes)
        {
            if (warehouse.Palettes.Contains(palette))
            {
                Console.WriteLine(
                    $"Palette {palette.Id} already added to the warehouse {warehouse.Id}! Skipping...");
            
                return;
            }
            
            Console.WriteLine($"Palette {palette.Id} added to the warehouse {warehouse.Id}.");
            
            warehouse.Palettes.Add(palette);
            palette.WarehouseId = warehouse.Id;
        }
    }
    
    /// <summary>
    /// Delete palette from the warehouse
    /// </summary>
    /// <param name="warehouse">Where to remove</param>
    /// <param name="id">What palette to remove</param>
    /// <exception cref="InvalidOperationException">Nothing to remove</exception>
    public void DeletePalette(Warehouse warehouse, Guid id)
    {
        var palette = warehouse.Palettes.SingleOrDefault(x => x.Id == id)
                      ?? throw new InvalidOperationException($"Palette with id = {id} wasn't found");

        Console.WriteLine($"Palette with {id} was removed from the warehouse.");

        warehouse.Palettes.Remove(palette);
    }
}