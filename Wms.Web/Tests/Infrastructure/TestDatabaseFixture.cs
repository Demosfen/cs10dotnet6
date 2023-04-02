using Wms.Web.Store;

namespace Wms.Web.Tests.Infrastructure;

public class TestDatabaseFixture : IDisposable
{
    private readonly string _dbFileName;

    public TestDatabaseFixture()
    {
        _dbFileName = $"{Guid.NewGuid().ToString()}.db";
        using var dbContext = new WarehouseDbContext(_dbFileName);
        dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        using var dbContext = new WarehouseDbContext(_dbFileName);
        dbContext.Database.EnsureDeleted();
    }

    public WarehouseDbContext CreateDbContext() => new(_dbFileName);
}