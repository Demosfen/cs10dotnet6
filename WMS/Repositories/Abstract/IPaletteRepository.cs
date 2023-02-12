using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Abstract;

public interface IPaletteRepository : IDbRepository
{
    Task<Palette?> GetAsync(Guid id, CancellationToken ct = default);

    Task CreateAsync(Palette palette, CancellationToken ct = default);

    Task UpdateAsync(Palette palette, CancellationToken ct = default);

    Task DeleteAsync(Guid id, CancellationToken cr = default);

    public void AddBox(Palette palette, Box box);

    public void DeleteBox(Palette palette, Box box);
}