using Microsoft.EntityFrameworkCore;

namespace WMS.Tests;

public class TestDatabaseFixture : IDisposable
{
    public TestDatabaseFixture()
    {
        // Здесь нам достаточно только проинициализировать сам контекст
        // Данные какие-то можно засунуть.. но это должны быть общие для всех тестов вещи
        // (например, какие-то словарные таблицы, которые менять не планируется)
        using var dbContext = new WarehouseDbContext.WarehouseDbContext();
        dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        using var dbContext = new WarehouseDbContext.WarehouseDbContext();
        dbContext.Database.EnsureDeleted();
    }
    
    public WarehouseDbContext.WarehouseDbContext CreateDbContext() => new WarehouseDbContext.WarehouseDbContext();
}