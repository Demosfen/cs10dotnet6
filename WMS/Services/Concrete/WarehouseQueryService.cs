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

    /// <summary>
    /// Group palettes by Expiry and then by Weight
    /// </summary>
    /// <param name="id">Warehouse ID</param>
    /// <returns>Group of palettes for chosen by ID warehouse</returns>
    /// <exception cref="Exception">No warehouse exist</exception>
    public IReadOnlyCollection<IGrouping<DateTime?, Palette>> SortByExpiryAndWeight(Guid id)
    {
        var palettes = _dbContext.Palettes
            .Where(w => w.WarehouseId == id);
        
        return palettes?
                   .Where(p => p.ExpiryDate.HasValue)
                   .OrderBy(p => p.ExpiryDate)
                   .ThenBy(p => p.Weight)
                   .Include(x => x.Boxes)
                   .GroupBy(g => g.ExpiryDate).ToList() 
               ?? throw new Exception("No warehouses found!");
    }

    /// <summary>
    /// Select three palettes from warehouse and order by
    /// Expiry and then by Volume
    /// </summary>
    /// <param name="id">Warehouse id</param>
    /// <returns>Three palettes sorted by Expiry and Volume</returns>
    /// <exception cref="Exception">No palettes exist</exception>
    public IReadOnlyCollection<Palette> ChooseThreePalettesByExpiryAndVolume(Guid id)
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