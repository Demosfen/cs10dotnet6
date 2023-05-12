using Microsoft.EntityFrameworkCore;
using Wms.Web.Store;

namespace Wms.Web.Api.IntegrationTests.Infrastructure;

public class TestDatabaseFixture : IDisposable
{
    private readonly string _connectionString;
    private readonly WarehouseDbContext _dbContext;

    public TestDatabaseFixture()
    {
        var dbFileName = $"{Guid.NewGuid().ToString()}.db";
        _connectionString = $"Data Source=../{dbFileName}";

        var options = new DbContextOptionsBuilder<WarehouseDbContext>()
            .UseSqlite(_connectionString)
            .Options;

        _dbContext = new WarehouseDbContext(options);
        _dbContext.Database.EnsureCreated();
    }

    public string GetConnectionString() => _connectionString;

    public void Dispose()
    {
        _dbContext?.Database?.EnsureDeleted();
        _dbContext?.Dispose();
    }
}