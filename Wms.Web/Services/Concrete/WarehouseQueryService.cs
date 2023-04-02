using Microsoft.EntityFrameworkCore;
using Wms.Web.Common.Exceptions;
using Wms.Web.Services.Abstract;
using Wms.Web.Store.Entities;
using Wms.Web.Store.Interfaces;

namespace Wms.Web.Services.Concrete;

public sealed class WarehouseQueryService : IWarehouseQueryService
{
    private IWarehouseDbContext _dbContext { get; }

    public WarehouseQueryService(IWarehouseDbContext dbContext) => _dbContext = dbContext;

    /// <summary>
    /// Group palettes by Expiry and then by Weight
    /// </summary>
    /// <param name="id">Warehouse ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Group of palettes for chosen by ID warehouse</returns>
    /// <exception cref="EntityNotFoundException">No warehouse exist</exception>
    public Task<List<IGrouping<DateTime?, Palette>>> SortByExpiryAndWeightAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Palettes
                   .Where(w => w.WarehouseId == id)
                   .Where(p => p.ExpiryDate.HasValue)
                   .OrderBy(p => p.ExpiryDate)
                   .ThenBy(p => p.Weight)
                   .Include(x => x.Boxes)
                   .GroupBy(g => g.ExpiryDate)
                   .ToListAsync(cancellationToken: cancellationToken)
               ?? throw new EntityNotFoundException(id);
    }

    /// <summary>
    /// Select three palettes from warehouse and order by
    /// Expiry and then by Volume
    /// </summary>
    /// <param name="id">Warehouse id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Three palettes sorted by Expiry and Volume</returns>
    /// <exception cref="EntityNotFoundException">No palettes exist</exception>
    public Task<List<Palette>> ChooseThreePalettesByExpiryAndVolumeAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Palettes
                   .Where(w => w.WarehouseId == id)
                   .OrderByDescending(p => p.ExpiryDate)
                   .Take(3)
                   .OrderByDescending(p => p.Volume)
                   .ToListAsync(cancellationToken: cancellationToken)
               ?? throw new EntityNotFoundException(id);
    }
}