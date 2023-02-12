using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Abstract;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Concrete;

/// <inheritdoc />
public sealed class WarehouseRepository : IWarehouseRepository
{
    private readonly IWarehouseDbContext _dbContext;

    public WarehouseRepository(IWarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Warehouse?> GetAsync(Guid id, CancellationToken ct = default) 
        => await _dbContext.Warehouses
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken: ct);

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
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct) 
                     ?? throw new Exception($"{nameof(Warehouse)} with {nameof(Warehouse.Id)}={id} doesn't exist");

        _dbContext.Warehouses.Remove(entity);
        await _dbContext.SaveChangesAsync(ct);
    }
}

