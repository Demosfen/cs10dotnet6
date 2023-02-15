using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Abstract;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;
using WMS.WarehouseDbContext.Specifications;

namespace WMS.Repositories.Concrete;

/// <inheritdoc />
public sealed class WarehouseRepository : IWarehouseRepository
{
    private readonly IWarehouseDbContext _dbContext;

    public WarehouseRepository(IWarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Warehouse?>> GetAllAsync(CancellationToken ct = default)
        => new[]
        {
            await _dbContext.Warehouses
                .Include(u => u.Palettes)
                .FirstOrDefaultAsync(ct)
        };

    public async Task<Warehouse?> GetAsync(Guid id, CancellationToken ct = default)
        => await _dbContext.Warehouses
               .AsNoTracking()
               .ById(id)
               .Include(u=>u.Palettes).FirstOrDefaultAsync(cancellationToken: ct)
           ?? throw new Exception($"No warehouse with {id} was found");

    public async Task CreateAsync(Warehouse warehouse, CancellationToken ct = default)
    {
        _dbContext.Warehouses.Add(warehouse);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Warehouse warehouse, CancellationToken ct = default)
    {
        _dbContext.Warehouses.Update(warehouse);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _dbContext.Warehouses
                         .ById(id)
                         .FirstOrDefaultAsync(cancellationToken: ct)
                     ?? throw new Exception($"{nameof(Warehouse)} with {nameof(Warehouse.Id)}={id} doesn't exist");

        _dbContext.Warehouses.Remove(entity);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAllAsync(CancellationToken ct = default)
    {
        var warehouse0 = _dbContext.Warehouses
            .OrderBy(e=>e.Id)
            .FirstOrDefault();
        var warehouse1 = _dbContext.Warehouses
            .OrderBy(e=>e.Id)
            .LastOrDefault();

       _dbContext.Warehouses.RemoveRange(
            warehouse0 ?? throw new Exception("No warehouses"), 
            warehouse1 ?? warehouse0);

       await _dbContext.SaveChangesAsync(ct); // TODO why doesn't it work???
    }

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
    
    public void DeletePalette(Warehouse warehouse, Guid id)
    {
        var palette = warehouse.Palettes.SingleOrDefault(x => x.Id == id)
                    ?? throw new InvalidOperationException($"Palette with id = {id} wasn't found");

        Console.WriteLine($"Palette with {id} was removed from the warehouse.");

        warehouse.Palettes.Remove(palette);
        palette.WarehouseId = null;
    }
}

