using Wms.Web.Services.Dto;

namespace Wms.Web.Services.Abstract;

public interface IWarehouseService : IBusinessService
{
    Task CreateAsync(WarehouseDto warehouseDto, CancellationToken ct = default);
    
    Task<IReadOnlyCollection<WarehouseDto>?> GetAllAsync(int offset, int size, bool deleted  = false, CancellationToken ct = default);

    Task<WarehouseDto?> GetByIdAsync(Guid id, int offset = 0, int size = 10, CancellationToken ct = default);
    
    Task UpdateAsync(WarehouseDto warehouseDto, CancellationToken ct = default);
    
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}