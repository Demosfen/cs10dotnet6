using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WMS.Repositories.Abstract;
using WMS.Store.Entities;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Concrete;

public sealed class WarehouseRepository : IWarehouseRepository, IWarehouseDbContext
{
    private readonly IWarehouseDbContext _dbContext;

    public WarehouseRepository(IWarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Warehouse?> GetAsync(Guid id) 
        => await _dbContext.Warehouses
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

    public async Task CreateAsync(Warehouse warehouse)
    {
        _dbContext.Warehouses.Add(warehouse);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Warehouse warehouse)
    {
        _dbContext.Warehouses.Update(warehouse);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbContext.Warehouses
            .FirstOrDefaultAsync(x => x.Id == id) 
                     ?? throw new Exception($"{nameof(Warehouse)} with {nameof(Warehouse.Id)}={id} doesn't exist");

        _dbContext.Warehouses.Remove(entity);
    }
}

