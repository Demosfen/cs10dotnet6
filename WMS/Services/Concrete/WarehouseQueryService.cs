using Microsoft.EntityFrameworkCore;
using WMS.Services.Abstract;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;
using WMS.Repositories.Concrete;

namespace WMS.Services.Concrete;

public sealed class WarehouseQueryService : IWarehouseQueryService
{
    private IWarehouseDbContext _dbContext { get; }

    public WarehouseQueryService(IWarehouseDbContext dbContext) => _dbContext = dbContext;
    public IReadOnlyCollection<IGrouping<DateTime?, Palette>> SortByExpiryAndWeight(Guid id)
    {
        var warehouse = _dbContext.Warehouses
            .Where(w => w.Id == id)
            .Include(w => w.Palettes).SingleOrDefault();
        
        return warehouse?.Palettes
            .Where(p => p.ExpiryDate.HasValue)
            .OrderBy(p => p.ExpiryDate)
            .ThenBy(p => p.Weight)
            .GroupBy(g => g.ExpiryDate).ToList() 
               ?? throw new Exception("No warehouses found!");
    }
    
    public IReadOnlyCollection<IGrouping<DateTime?, Palette>> SortByExpiryAndWeightAlternative(Guid id)
    {
        var palettes = _dbContext.Palettes
            .Where(w => w.WarehouseId == id);
        
        return palettes?
                   .Where(p => p.ExpiryDate.HasValue)
                   .OrderBy(p => p.ExpiryDate)
                   .ThenBy(p => p.Weight)
                   .GroupBy(g => g.ExpiryDate).ToList() 
               ?? throw new Exception("No warehouses found!");
    }

    public IReadOnlyCollection<Palette> ChooseThreePalettesByExpiryAndVolume(Guid id)
    {
        var warehouse = _dbContext.Warehouses
            .Where(w => w.Id == id)
            .Include(w => w.Palettes).SingleOrDefault();
        
        return  warehouse?.Palettes
            .OrderByDescending(p => p.ExpiryDate)
            .ThenBy(p => p.Volume)
            .Take(3).ToList()
            ?? throw new Exception("No palettes found");
    }
    
    public IReadOnlyCollection<Palette> ChooseThreePalettesByExpiryAndVolumeAlternative(Guid id)
    {
        var palettes = _dbContext.Palettes
            .Where(w => w.WarehouseId == id);
        
        return  palettes?
                    .OrderByDescending(p => p.ExpiryDate)
                    .ThenBy(p => p.Volume)
                    .Take(3).ToList()
                ?? throw new Exception("No palettes found");
    }
}