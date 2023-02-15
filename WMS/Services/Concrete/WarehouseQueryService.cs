using Microsoft.EntityFrameworkCore;
using WMS.Services.Abstract;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;
using WMS.Repositories.Concrete;

namespace WMS.Services.Concrete;

public sealed class WarehouseQueryService : IWarehouseQueryService
{
    public IReadOnlyCollection<IGrouping<DateTime?, Palette>> SortByExpiryAndWeight(IWarehouseDbContext dbContext, Guid id)
    {
        var wmsRepo = new WarehouseRepository(dbContext);
        var wmTest = wmsRepo.GetAsync(id);
        
        var warehouse = dbContext.Warehouses
            .Where(w => w.Id == id)
            .Include(w => w.Palettes).SingleOrDefault();
        
        return warehouse?.Palettes
            .Where(p => p.ExpiryDate.HasValue)
            .OrderBy(p => p.ExpiryDate)
            .ThenBy(p => p.Weight)
            .GroupBy(g => g.ExpiryDate).ToList() 
               ?? throw new Exception("No warehouses found!");
    }

    public IReadOnlyCollection<Palette> ChooseThreePalettesByExpiryAndVolume(IWarehouseDbContext dbContext, Guid id)
    {
        var wmsRepo = new WarehouseRepository(dbContext);

        var warehouse = dbContext.Warehouses
            .Where(w => w.Id == id)
            .Include(w => w.Palettes).SingleOrDefault();
        
        return  warehouse?.Palettes
            .OrderByDescending(p => p.ExpiryDate)
            .ThenBy(p => p.Volume)
            .Take(3).ToList()
            ?? throw new Exception("No palettes found");
    }
}