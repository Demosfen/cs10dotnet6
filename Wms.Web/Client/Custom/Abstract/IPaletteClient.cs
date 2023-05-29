using Wms.Web.Common.Exceptions;
using Wms.Web.Contracts.Requests;
using Wms.Web.Contracts.Responses;

namespace Wms.Web.Client.Custom.Abstract;

public interface IPaletteClient
    {
        /// <summary>
        /// Get palettes from warehouse
        /// </summary>
        /// <param name="warehouseId">Warehouse ID</param>
        /// <param name="offset">Palettes offset</param>
        /// <param name="size">Palettes collection size</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<PaletteResponse>?> GetAllAsync(
            Guid warehouseId,
            int? offset, int? size, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get deleted palettes from warehouse
        /// </summary>
        /// <param name="warehouseId">Warehouse ID</param>
        /// <param name="offset">Palettes offset</param>
        /// <param name="size">Palettes collection size</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<PaletteResponse>?> GetAllDeletedAsync(
            Guid warehouseId,
            int? offset, int? size, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get palette by ID
        /// </summary>
        /// <param name="paletteId">Palette ID</param>
        /// <param name="offset">Box offset</param>
        /// <param name="size">Box collection size</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="EntityNotFoundException">Palette does not exist</exception>
        /// <returns>Palette entity</returns>
        Task<PaletteResponse?> GetByIdAsync(
            Guid paletteId,
            int? offset, int? size,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Create palette
        /// </summary>
        /// <param name="warehouseId">Warehouse ID</param>
        /// <param name="paletteId">Palette ID</param>
        /// <param name="request">Palette request body</param>
        /// <param name="cancellationToken">Token</param>
        /// <exception cref="EntityAlreadyExistException">Double create exception</exception>
        /// <exception cref="ApiValidationException">BadRequest exception</exception>
        /// <returns>Created palette entity</returns>
        Task<PaletteResponse?> CreateAsync(Guid warehouseId,
            Guid paletteId,
            PaletteRequest request,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Update palette
        /// </summary>
        /// <param name="paletteId">Palette ID</param>
        /// <param name="warehouseId">Warehouse ID</param>
        /// <param name="request">Updated palette request body</param>
        /// <param name="cancellationToken">Token</param>
        /// <returns>Updated palette response body</returns>
        Task<PaletteResponse?> PutAsync(
            Guid paletteId,
            Guid warehouseId,
            PaletteRequest request,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete palette
        /// </summary>
        /// <param name="paletteId">Palette ID</param>
        /// <param name="cancellationToken">Token</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> DeleteAsync(Guid paletteId, CancellationToken cancellationToken = default);

    }