using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

public class TestDatabaseFixture : IDisposable
{
    private static readonly object Lock = new();
    private static bool _databaseInitialized;
    private bool _disposed;

    protected TestDatabaseFixture()
    {
        lock (Lock)
        {
            if (!_databaseInitialized)
            {
                var unitOfWork = new UnitOfWork();
                var fixture = new Fixture().Customize(new UnitsCustomization());
                
                using (var context = CreateContext())
                {
                    context.Database.EnsureCreated();
                    var warehouse = fixture.Create<Warehouse>();

                    for (var i = 0; i < 5; i++)
                    {
                        var palette = fixture.Build<Palette>()
                            .With(p => p.WarehouseId, warehouse.Id)
                            .Create();

                        var boxes = fixture.Build<Box>()
                            .With(x => x.PaletteId, palette.Id)
                            .CreateMany(5);

                        foreach (var box in boxes)
                        {
                            unitOfWork.PaletteRepository?.AddBox(palette, box);
                        }

                        unitOfWork.WarehouseRepository?.AddPalette(warehouse, palette);

                        unitOfWork.BoxRepository?.InsertMultipleAsync(boxes).ConfigureAwait(false);

                        unitOfWork.PaletteRepository?.InsertAsync(palette).ConfigureAwait(false);

                        unitOfWork.WarehouseRepository?.InsertAsync(warehouse).ConfigureAwait(false);
                    }

                    unitOfWork.Save();
                }
                _databaseInitialized = true;
            }
        }
    }

    public WarehouseDbContext.WarehouseDbContext CreateContext() => new();
    
    public void Dispose() => Dispose(true);
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                
            }
            _disposed = true;
        }
    }
    
    // Destructor
    ~TestDatabaseFixture() => Dispose (false);
}