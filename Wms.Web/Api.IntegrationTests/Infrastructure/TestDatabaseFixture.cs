using Microsoft.EntityFrameworkCore;
using Wms.Web.Store;
using Wms.Web.Store.Interfaces;

namespace Wms.Web.Api.IntegrationTests.Infrastructure;

public class TestDatabaseFixture : IDisposable
{
    private readonly IConfigurationRoot _configuration;
    private readonly string _connectionString;
    private readonly WarehouseDbContext _dbContext;

    public TestDatabaseFixture()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        var dbFileName = $"{Guid.NewGuid().ToString()}.db";
        _connectionString = $"Data Source=../{dbFileName}";
        
        var options = new DbContextOptionsBuilder<WarehouseDbContext>()
            .UseSqlite(_connectionString)
            .Options;

        _dbContext = new WarehouseDbContext(options);
        _dbContext.Database.EnsureCreated();
        _dbContext.Database.MigrateAsync();
        
        _configuration["ConnectionStrings:WarehouseDbContextCS"] = _connectionString;
    }

    public string GetConnectionString() => _connectionString;

    public void Dispose()
    {
        _dbContext?.Database?.EnsureDeleted();
        _dbContext?.Dispose();
    }
}