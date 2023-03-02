using AutoFixture;
using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

public abstract class DatabaseFixture //: IDisposable
{
    private static readonly object Lock = new();
    private static bool _databaseInitialized;
    // private bool _disposed;

    protected DatabaseFixture()
    {
        lock (Lock)
        {
            if (!_databaseInitialized)
            {
                var unitOfWork = new UnitOfWork();
                var fixture = new Fixture().Customize(new UnitsCustomization());
                
                using (var context = new WarehouseDbContext.WarehouseDbContext())
                {
                    context.Database.EnsureDeleted();
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

    // public void Dispose() => Dispose(true);
    //
    // protected virtual void Dispose(bool disposing)
    // {
    //     if (!_disposed)
    //     {
    //         if (disposing)
    //         {
    //         }
    //         _disposed = true;
    //     }
    // }
    //
    // // Destructor
    // ~DatabaseFixture()
    // {
    //     Dispose (false);
    // }
}