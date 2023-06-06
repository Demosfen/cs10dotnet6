using Wms.Web.Business.Dto;
using Wms.Web.Common.Exceptions;

namespace Wms.Web.Business.Abstract;

/// <summary>
/// Warehouse service (business logic)
/// </summary>
public interface IWarehouseService : IBusinessService
{
    /// <summary>
    /// Warehouse service (business logic)
    /// </summary>
    /// <param name="warehouseDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreateAsync(WarehouseDto warehouseDto, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all warehouses from Database with specified select offset and warehouse list size
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="deleted"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Read only collection of warehouses</returns>
    Task<IReadOnlyCollection<WarehouseDto>?> GetAllAsync(
        int offset, int size, 
        bool deleted  = false, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the warehouse by ID with specified select palette offset and palette list size
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <returns>Warehouse dto</returns>
    Task<WarehouseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Update warehouse by ID
    /// </summary>
    /// <param name="warehouseDto"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="EntityWasDeletedException"></exception>
    /// <exception cref="EntityNotEmptyException"></exception>
    /// <returns></returns>
    Task UpdateAsync(WarehouseDto warehouseDto, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Delete warehouse by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="EntityNotEmptyException"></exception>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}