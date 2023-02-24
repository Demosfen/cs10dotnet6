using AutoFixture;
using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

public sealed class DatabaseFixture : IDisposable
{
    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public DatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                var unitOfWork = new UnitOfWork();
                var fixture = new Fixture().Customize(new UnitsCustomization());
                
                using (var context = new WarehouseDbContext.WarehouseDbContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    for (int i = 0; i < 5; i++)
                    {
                        var warehouse = fixture.Create<Warehouse>();

                        var palette0 = fixture.Build<Palette>()
                            .With(p => p.WarehouseId, warehouse.Id).Create();

                        var boxes0 = fixture.Build<Box>()
                            .With(x => x.PaletteId, palette0.Id).CreateMany(5);

                        foreach (var box in boxes0)
                        {
                            unitOfWork.PaletteRepository?.AddBox(palette0, box);
                        }

                        unitOfWork.WarehouseRepository?.AddPalette(warehouse, palette0);

                        unitOfWork.BoxRepository?.InsertMultipleAsync(boxes0).ConfigureAwait(false);

                        unitOfWork.PaletteRepository?.InsertAsync(palette0).ConfigureAwait(false);

                        unitOfWork.WarehouseRepository?.InsertAsync(warehouse).ConfigureAwait(false);
                    }

                    unitOfWork.Save();
                }
                _databaseInitialized = true;
            }
        }
    }

    public void Dispose()
    {
    }
}