using Wms.Web.Common.Exceptions;
using Wms.Web.Store.Entities;

namespace Wms.Web.Repositories.Abstract;

public interface IWarehouseRepository : IGenericRepository<Warehouse>
{
    /// <summary>
    /// Put palette to the warehouse
    /// </summary>
    /// <param name="warehouseId">Warehouse ID</param>
    /// <param name="palette">What palette to put</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundException">Nothing to remove</exception>
    Task AddPaletteAsync(
        Guid warehouseId,
        Palette palette,
        CancellationToken cancellationToken);

    /// <summary>
    /// Add range of palettes
    /// </summary>
    /// <param name="warehouseId">Warehouse Id</param>
    /// <param name="palettes"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundException">Nothing to remove</exception>
    Task AddPalettesAsync(
        Guid warehouseId,
        IEnumerable<Palette> palettes,
        CancellationToken cancellationToken);

    /// <summary>
    /// Delete palette from the warehouse
    /// </summary>
    /// <param name="warehouseId">Warehouse ID</param>
    /// <param name="palette">Palette</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="EntityNotFoundException">Nothing to remove</exception>
    Task DeletePaletteAsync(
        Guid warehouseId,
        Palette palette,
        CancellationToken cancellationToken);
}