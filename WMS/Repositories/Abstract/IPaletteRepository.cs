using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Abstract;

public interface IPaletteRepository :  IGenericRepository<Palette>
{
    /// <summary>
    /// Performs simple addition of the box to the palette
    /// </summary>
    /// <param name="paletteId">Where to put the box</param>
    /// <param name="box">What box to put</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="ArgumentException">Sizes inconsistency</exception>
    Task AddBox(
        Guid paletteId, 
        Box box, 
        CancellationToken cancellationToken);

    /// <summary>
    /// Simple deletion of the box from the palette
    /// </summary>
    /// <param name="paletteId">Palete Id</param>
    /// <param name="box"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="InvalidOperationException">Nothing to remove</exception>
    Task DeleteBox(
        Guid paletteId, 
        Box box, 
        CancellationToken cancellationToken);
}