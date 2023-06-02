using Wms.Web.Business.Dto;

namespace Wms.Web.Business.Abstract;

public interface IWarehouseService : IBusinessService
{
    Task CreateAsync(WarehouseDto warehouseDto, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<WarehouseDto>?> GetAllAsync(
        int offset, int size, 
        bool deleted  = false, 
        CancellationToken cancellationToken = default);

    Task<WarehouseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(WarehouseDto warehouseDto, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}