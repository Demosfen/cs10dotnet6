using WMS.Common.Exceptions;
using WMS.Store.Entities;

namespace WMS.Repositories.Abstract;

public interface IWarehouseRepository : IGenericRepository<Warehouse>
{
    /// <summary>
    /// Put palette to the warehouse
    /// </summary>
    /// <param name="warehouseId">Warehouse ID</param>
    /// <param name="palette">What palette to put</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundException">Nothing to remove</exception>
    Task AddPalette(
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
    Task AddPalettes(
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
    Task DeletePalette(
        Guid warehouseId,
        Palette palette,
        CancellationToken cancellationToken);
}