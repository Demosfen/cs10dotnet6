using Wms.Web.Common.Exceptions;
using Wms.Web.Contracts.Requests;
using Wms.Web.Contracts.Responses;

namespace Wms.Web.Client.Custom.Abstract;

public interface IBoxClient
{
    /// <summary>
    /// Get all boxes
    /// </summary>
    /// <param name="paletteId">Palette ID</param>
    /// <param name="offset">Box offset</param>
    /// <param name="size">Box collection size</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns>Collection of boxes</returns>
    Task<IReadOnlyCollection<BoxResponse>?> GetAllAsync(
        Guid paletteId,
        int? offset, 
        int? size, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all deleted boxes
    /// </summary>
    /// <param name="paletteId">Palette ID</param>
    /// <param name="offset">Box offset</param>
    /// <param name="size">Box collection size</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns>Collection of deleted boxes</returns>
    Task<IReadOnlyCollection<BoxResponse>?> GetAllDeletedAsync(
        Guid paletteId,
        int? offset, 
        int? size, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get box by ID
    /// </summary>
    /// <param name="boxId">Box ID</param>
    /// <param name="cancellationToken">Token</param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <returns>Box entity</returns>
    Task<BoxResponse?> GetByIdAsync(
        Guid boxId, 
        CancellationToken cancellationToken = default);
        
    /// <summary>
    /// Create box
    /// </summary>
    /// <param name="paletteId">Palette ID</param>
    /// <param name="boxId">Box ID</param>
    /// <param name="request">Create box request bodu</param>
    /// <param name="cancellationToken">Token</param>
    /// <exception cref="EntityAlreadyExistException"></exception>
    /// <exception cref="EntityExpiryDateException"></exception>
    /// <exception cref="UnprocessableEntityException"></exception>
    /// <exception cref="ApiValidationException"></exception>
    /// <returns>Created box entity</returns>
    Task<BoxResponse?> CreateAsync(Guid paletteId,
        Guid boxId,
        BoxRequest request,
        CancellationToken cancellationToken = default);
        
    /// <summary>
    /// Updates box
    /// </summary>
    /// <param name="boxId">Box ID</param>
    /// <param name="request">Create box request bodu</param>
    /// <param name="cancellationToken">Token</param>
    /// <exception cref="EntityAlreadyExistException"></exception>
    /// <exception cref="EntityExpiryDateException"></exception>
    /// <exception cref="UnprocessableEntityException"></exception>
    /// <exception cref="ApiValidationException"></exception>
    /// <returns>Updated box entity</returns>
    Task<BoxResponse?> PutAsync(
        Guid boxId,
        Guid paletteId,
        BoxRequest request,
        CancellationToken cancellationToken = default);
        
    /// <summary>
    /// Delete box
    /// </summary>
    /// <param name="boxId">Box ID</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns></returns>
    Task<HttpResponseMessage> DeleteAsync(Guid boxId, CancellationToken cancellationToken = default);
    
}