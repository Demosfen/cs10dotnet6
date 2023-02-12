using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Abstract;

public interface IBoxRepository : IDbRepository
{
    Task<Box?> GetAsync(Guid id, CancellationToken ct = default);

    Task CreateAsync(Box box, CancellationToken ct = default);

    Task UpdateAsync(Box box, CancellationToken ct = default);

    Task DeleteAsync(Guid id, CancellationToken ct = default);
}