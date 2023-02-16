using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Abstract;

public interface IWarehouseRepository : IDbRepository
{
    Task<IReadOnlyCollection<Warehouse?>> GetAllAsync(CancellationToken ct = default);
    Task<Warehouse?> GetAsync(Guid id, CancellationToken ct = default);

    Task CreateAsync(Warehouse warehouse, CancellationToken ct = default);

    Task UpdateAsync(Warehouse warehouse, CancellationToken ct = default);

    Task DeleteAsync(Guid id, CancellationToken ct = default);

    public void AddPalette(Warehouse warehouse, Palette palette);

    public void DeletePalette(Warehouse warehouse, Guid id);
}