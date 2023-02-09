using WMS.Services.Abstract;
using WMS.Store.Entities;

namespace WMS.Repositories.Abstract;

public interface IWarehouseRepository : IDbRepository
{
    Task<Warehouse?> GetAsync(Guid id, CancellationToken ct = default);

    Task CreateAsync(Warehouse warehouse, CancellationToken ct = default);

    Task UpdateAsync(Warehouse warehouse, CancellationToken ct = default);

    Task DeleteAsync(Guid id, CancellationToken ct = default);
}