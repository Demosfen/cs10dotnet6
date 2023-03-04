using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

[Collection(DbTestCollection.Name)]
public class WarehouseRepositoryTests: IClassFixture<TestDatabaseFixture>, IAsyncLifetime
{
    private readonly WarehouseDbContext.WarehouseDbContext _dbContext;

    private readonly TestDataHelper _dataHelper = new();
    
    public WarehouseRepositoryTests(TestDatabaseFixture fixture)
    {
        _dbContext = fixture.CreateDbContext();
    }
    
    [Fact]
    public async Task RepositoryInsert_ShouldInsertWarehouse()
    {
        // Arrange
        var sut = new UnitOfWork(_dbContext);

        sut.WarehouseRepository?.InsertAsync(new Warehouse("TestWarehouse"));
        await sut.SaveAsync();

        var result = await _dbContext.Warehouses.FirstOrDefaultAsync(x => x.Name == "TestWarehouse");

        // Assert
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task RepositoryInsert_ShouldInsertWarehouseWithFivePalettes()
    {
        // Arrange
        var sut = new UnitOfWork(_dbContext);
        var warehouse = _dataHelper.CreateWarehouseWithPalettesAndBoxes("TestWarehouse", 5, 1);

        sut.WarehouseRepository?.InsertAsync(warehouse);
        await sut.SaveAsync();

        var palettes = sut.BoxRepository?.GetAllAsync();
        if (palettes != null)
        {
            var result = palettes.Result.Count();

            // Assert
            result.Should().Be(5);
        }
    }
    
    [Fact]
    public async Task RepositoryInsert_ShouldInsert_WarehouseWithTwentyFiveBoxesOnFivePalettes()
    {
        // Arrange
        var sut = new UnitOfWork(_dbContext);
        var warehouse = _dataHelper.CreateWarehouseWithPalettesAndBoxes("TestWarehouse", 5, 5);

        sut.WarehouseRepository?.InsertAsync(warehouse);
        await sut.SaveAsync();

        var boxes = sut.BoxRepository?.GetAllAsync();
        if (boxes != null)
        {
            var result = boxes.Result.Count();

            // Assert
            result.Should().Be(25);
        }
    }
    
    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _dbContext.DisposeAsync();
}