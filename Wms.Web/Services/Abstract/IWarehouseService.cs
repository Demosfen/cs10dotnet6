using Wms.Web.Services.Dto;

namespace Wms.Web.Services.Abstract;

public interface IWarehouseService : IBusinessService
{
    Task CreateAsync(WarehouseDto warehouseDto, CancellationToken ct = default);
    
    Task<IReadOnlyCollection<WarehouseDto>?> GetAllAsync(CancellationToken ct = default);

    Task<WarehouseDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    
    Task UpdateAsync(WarehouseDto warehouseDto, CancellationToken ct = default);
    
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}