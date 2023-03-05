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
    /// <param name="palette">Where to remove</param>
    /// <param name="box">What to remove</param>
    /// <exception cref="InvalidOperationException">Nothing to remove</exception>
    void DeleteBox(Palette palette, Box box);
}