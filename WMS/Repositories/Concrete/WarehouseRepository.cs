using System.Text;
using WMS.Common.Exceptions;
using WMS.Repositories.Abstract;
using WMS.Store.Entities;

namespace WMS.Repositories.Concrete;

public sealed class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(Store.WarehouseDbContext dbContext)
        : base(dbContext)
    {
    }

    /// <inheritdoc cref="IWarehouseRepository"/>
    public async Task AddPaletteAsync(
        Guid warehouseId, 
        Palette palette, 
        CancellationToken cancellationToken)
    {
        var warehouse = await GetByIdAsync(warehouseId, cancellationToken)
                        ?? throw new EntityNotFoundException(warehouseId);
        
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
    
    /// <inheritdoc cref="IWarehouseRepository"/>
    public async  Task AddPalettesAsync(
        Guid warehouseId, 
        IEnumerable<Palette> palettes, 
        CancellationToken cancellationToken)
    {
        foreach (var palette in palettes)
        {
            await AddPaletteAsync(warehouseId, palette, cancellationToken);
        }
    }

    /// <inheritdoc cref="IWarehouseRepository"/>
    public async Task DeletePaletteAsync(
        Guid warehouseId,
        Palette palette,
        CancellationToken cancellationToken)
    {
        var warehouse = await GetByIdAsync(warehouseId, cancellationToken)
                        ?? throw new EntityNotFoundException(warehouseId);

        if (!warehouse.Palettes.Contains(palette))
        {
            Console.WriteLine(
                $"Warehouse {warehouse.Id} has no palette {palette.Id}! Skipping...");
            
            return;
        }

        warehouse.Palettes.Remove(palette);
    }
}