using WMS.WarehouseDbContext.Entities;

namespace WMS.Repositories.Concrete;

public sealed class UnitOfWork : IDisposable
{
    private readonly WarehouseDbContext.WarehouseDbContext _dbContext = new();
    private GenericRepository<Box>? _boxRepository;
    private GenericRepository<Palette>? _paletteRepository;
    private GenericRepository<Warehouse>? _warehouseRepository;

    public GenericRepository<Box>? BoxRepository
    {
        get
        {
            return _boxRepository ??= new GenericRepository<Box>(_dbContext);
        }
    }

    public GenericRepository<Palette>? PaletteRepository
    {
        get
        {
            return _paletteRepository ??= new GenericRepository<Palette>(_dbContext);
        }
    }

    public GenericRepository<Warehouse>? WarehouseRepository
    {
        get
        {
            return _warehouseRepository ??= new GenericRepository<Warehouse>(_dbContext);
        }
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
    }
}