using WMS.Common.Exceptions;
using WMS.Store.Entities;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Abstract;

public interface IPaletteRepository :  IGenericRepository<Palette>
{
    /// <summary>
    /// Performs simple addition of the box to the palette
    /// </summary>
    /// <param name="paletteId">Palette ID</param>
    /// <param name="box">What box to put</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="UnitOversizeException">Box oversize</exception>
    Task AddBox(
        Guid paletteId, 
        Box box, 
        CancellationToken cancellationToken);

    /// <summary>
    /// Performs simple addition of the boxes list to the palette
    /// </summary>
    /// <param name="paletteId">Palette ID</param>
    /// <param name="boxes">List boxes</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="UnitOversizeException">Box oversize</exception>
    Task AddBoxes(
        Guid paletteId, 
        IEnumerable<Box> boxes, 
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Simple deletion of the box from the palette
    /// </summary>
    /// <param name="paletteId">Palette id</param>
    /// <param name="box">Box</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="EntityNotFoundException">Nothing to remove</exception>
    Task DeleteBox(
        Guid paletteId, 
        Box box, 
        CancellationToken cancellationToken);
}