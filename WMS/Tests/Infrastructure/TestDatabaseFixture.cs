namespace WMS.Tests.Infrastructure;

public class TestDatabaseFixture : IDisposable
{
    private readonly string _dbFileName;

    public TestDatabaseFixture()
    {
        _dbFileName = $"{Guid.NewGuid().ToString()}.db";
        // Здесь нам достаточно только проинициализировать сам контекст
        // Данные какие-то можно засунуть.. но это должны быть общие для всех тестов вещи
        // (например, какие-то словарные таблицы, которые менять не планируется)
        using var dbContext = new Store.WarehouseDbContext(_dbFileName);
        dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        using var dbContext = new Store.WarehouseDbContext(_dbFileName);
        dbContext.Database.EnsureDeleted();
    }

    public Store.WarehouseDbContext CreateDbContext() => new(_dbFileName);
}