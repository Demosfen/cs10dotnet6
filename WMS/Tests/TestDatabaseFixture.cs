namespace WMS.Tests;

public class TestDatabaseFixture : IDisposable
{
    public WarehouseDbContext.WarehouseDbContext DbContext { get; }

    public TestDatabaseFixture()
    {
        // Здесь нам достаточно только проинициализировать сам контекст
        // Данные какие-то можно засунуть.. но это должны быть общие для всех тестов вещи
        // (например, какие-то словарные таблицы, которые менять не планируется)
        DbContext = new WarehouseDbContext.WarehouseDbContext();
        DbContext.Database.EnsureCreated();
    }
    
    public void Dispose() => DbContext?.Dispose();
}