using Wms.Web.Common.Exceptions;
using Wms.Web.Services.Dto;

namespace Wms.Web.Services.Abstract;

public interface IBoxService : IBusinessService
{
    /// <summary>
    /// Creates box with specified parameters in Database
    /// </summary>
    /// <param name="boxDto">Box Dto</param>
    /// <param name="ct">Cancellation Token</param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="UnitOversizeException"></exception>
    /// <returns></returns>
    Task CreateAsync(BoxDto boxDto, CancellationToken ct = default);

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
    /// <returns>Box</returns>
    Task<BoxDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Updates box entity in Database
    /// </summary>
    /// <param name="boxDto">Box</param>
    /// <param name="ct">Cancellation Token</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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