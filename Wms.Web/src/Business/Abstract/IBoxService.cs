using Wms.Web.Business.Dto;
using Wms.Web.Common.Exceptions;

namespace Wms.Web.Business.Abstract;

/// <summary>
/// Box service (business logic) 
/// </summary>
public interface IBoxService : IBusinessService
{
    /// <summary>
    /// Creates box with specified parameters in Database
    /// </summary>
    /// <param name="boxDto">Box Dto</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <exception cref="EntityAlreadyExistException"></exception>
    /// <exception cref="UnitOversizeException"></exception>
    /// <exception cref="EntityWasDeletedException"></exception>
    /// <exception cref="EntityExpiryDateException"></exception> 
    Task CreateAsync(BoxDto boxDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all boxes from Database
    /// </summary>
    /// <param name="size"></param>
    /// <param name="deleted"></param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <param name="id"></param>
    /// <param name="offset"></param>
    /// <returns>Box list</returns>
    Task<IReadOnlyCollection<BoxDto>> GetAllAsync(
        Guid id, 
        int offset = 0, 
        int size = 100, 
        bool deleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets box by its Id
    /// </summary>
    /// <param name="id">Box Id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <returns>Box dto</returns>
    Task<BoxDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Updates box entity in Database
    /// </summary>
    /// <param name="boxDto">Box</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <exception cref="EntityAlreadyExistException"></exception>
    /// <exception cref="UnitOversizeException"></exception>
    /// <exception cref="EntityWasDeletedException"></exception>
    /// <exception cref="EntityExpiryDateException"></exception> 
    /// <returns>boxDto</returns>
    Task UpdateAsync(BoxDto boxDto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Deletes box from database
    /// </summary>
    /// <param name="id">Box Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}