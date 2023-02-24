using System.Xml.Linq;
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
        get => _boxRepository ??= new GenericRepository<Box>(_dbContext);
    }

    public GenericRepository<Palette>? PaletteRepository
    {
        get => _paletteRepository ??= new GenericRepository<Palette>(_dbContext);
    }

    public GenericRepository<Warehouse>? WarehouseRepository
    {
        get => _warehouseRepository ??= new GenericRepository<Warehouse>(_dbContext);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    private bool _disposed = false;

    private void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}