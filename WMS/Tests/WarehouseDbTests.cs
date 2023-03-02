using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

[Collection(DbTestCollection.Name)]
public class WarehouseDbTests: IClassFixture<TestDatabaseFixture>, IAsyncLifetime
{
    private readonly WarehouseDbContext.WarehouseDbContext _dbContext;

    public WarehouseDbTests(TestDatabaseFixture fixture)
    {
        // Вся Fixture тебе не нужна - достаточно же только контекста :)
        _dbContext = fixture.CreateDbContext(); 
    }
    
    [Fact]
    public async Task Test1()
    {
        // Arrange
        
        // Тестовые данные создаём внутри каждого теста, уникальные для него,
        // т.к. Fixture (а, следовательно, и весь контекст) шарится между тестами
        _dbContext.Warehouses.Add(new Warehouse("test")); 
        await _dbContext.SaveChangesAsync();

        // Act
        var warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(x => x.Name == "test");

        // Assert
        warehouse.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Test2()
    {
        // Arrange
        
        // Тестовые данные создаём внутри каждого теста, уникальные для него,
        // т.к. Fixture (а, следовательно, и весь контекст) шарится между тестами
        _dbContext.Warehouses.Add(new Warehouse("test")); 
        await _dbContext.SaveChangesAsync();

        // Act
        var warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(x => x.Name == "test");

        // Assert
        warehouse.Should().NotBeNull();
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _dbContext.DisposeAsync();
}