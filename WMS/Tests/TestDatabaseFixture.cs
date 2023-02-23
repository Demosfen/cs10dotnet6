using AutoFixture;
using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Concrete;

namespace WMS.Tests;

public class TestDatabaseFixture
{
    private const string DbFileName = "TestDatabase";
    private const string ConnectionString = $"Data Source=../{DbFileName}";
    
    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    
                    var unitOfWork = new UnitOfWork()
                }

                _databaseInitialized = true;
            }
        }
    }

    public WarehouseDbContext.WarehouseDbContext CreateContext()
        => new WarehouseDbContext.WarehouseDbContext(
            new DbContextOptionsBuilder<WarehouseDbContext.WarehouseDbContext>()
                .UseSqlite(ConnectionString)
                .Options);
}