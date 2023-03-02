using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

public class WarehouseDbTests: IClassFixture<TestDatabaseFixture>
{
    private readonly WarehouseDbContext.WarehouseDbContext _dbContext;

    public WarehouseDbTests(TestDatabaseFixture fixture)
    {
        // Вся Fixture тебе не нужна - достаточно же только контекста :)
        _dbContext = fixture.DbContext; 
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
}