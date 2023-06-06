using Wms.Web.Business.Dto;
using Wms.Web.Common.Exceptions;

namespace Wms.Web.Business.Abstract;

/// <summary>
/// Palette service (business logic) 
/// </summary>
public interface IPaletteService : IBusinessService
{
    /// <summary>
    /// Creates palette with specified parameters in Database
    /// </summary>
    /// <param name="paletteDto">Palette Dto</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="EntityWasDeletedException"></exception>
    /// <exception cref="EntityAlreadyExistException"></exception>
    Task CreateAsync(PaletteDto paletteDto, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all palettes from Database with specified select offset and palette list size
    /// </summary>
    /// <param name="id"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="deleted"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Palettes list</returns>
    Task<IReadOnlyCollection<PaletteDto>> GetAllAsync(
        Guid id, 
        int offset, int size,
        bool deleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the palette by ID with specified select box offset and box list size
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <returns>Palette dto</returns>
    Task<PaletteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Refreshes palette by ID when palette is updated
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <returns></returns>
    Task RefreshAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Updates palette by ID
    /// </summary>
    /// <param name="paletteDto"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="EntityWasDeletedException"></exception>
    /// <exception cref="EntityNotEmptyException"></exception>
    /// <returns></returns>
    Task UpdateAsync(PaletteDto paletteDto, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes palette by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="EntityNotEmptyException"></exception>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}