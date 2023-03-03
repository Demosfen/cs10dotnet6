using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Concrete;
using WMS.Services.Concrete;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

[Collection(DbTestCollection.Name)]
public class WarehouseServiceTests: IClassFixture<TestDatabaseFixture>, IAsyncLifetime
{
    private readonly WarehouseDbContext.WarehouseDbContext _dbContext;

    private readonly UnitOfWork _unitOfWork = new();

    public WarehouseServiceTests(TestDatabaseFixture fixture)
    {
        _dbContext = fixture.CreateDbContext(); 
    }
    
    [Fact]
    public async Task Test1()
    {
        // Arrange
        var sut = new WarehouseQueryService(_dbContext);
        var fixture = new Fixture();
        var warehouse = fixture.Build<Warehouse>()
            .With(p => p.IsDeleted, false)
            .With(p => p.Name, "Test1")
            .Create();
        
        _unitOfWork.WarehouseRepository?.InsertAsync(warehouse);

        // Act
        warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(x => x.Name == "test");

        // Assert
        warehouse.Should().NotBeNull();
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _dbContext.DisposeAsync();
}