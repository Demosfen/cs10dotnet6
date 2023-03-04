using FluentAssertions;
using WMS.Repositories.Concrete;
using WMS.Services.Concrete;


namespace WMS.Tests;

[Collection(DbTestCollection.Name)]
public class WarehouseServiceTests: IClassFixture<TestDatabaseFixture>, IAsyncLifetime
{
    private readonly WarehouseDbContext.WarehouseDbContext _dbContext;

    private readonly TestDataHelper _dataHelper = new();
    
    public WarehouseServiceTests(TestDatabaseFixture fixture)
    {
        _dbContext = fixture.CreateDbContext();
    }
    
    [Fact]
    public async Task SortByExpiryAndWeight_ReturnsGroupOfPalettes()
    {
        // Arrange
        var sut = new WarehouseQueryService(_dbContext);
        var unitOfWork = new UnitOfWork(_dbContext);
        var warehouse = _dataHelper.CreateWarehouseWithPalettesAndBoxes(
            "TestWarehouse", 5, 5);

        unitOfWork.WarehouseRepository?.InsertAsync(warehouse);
        await unitOfWork.SaveAsync();

        var expected = warehouse.Palettes?
            .Where(p => p.ExpiryDate.HasValue)
            .OrderBy(p => p.ExpiryDate)
            .ThenBy(p => p.Weight)
            .GroupBy(g => g.ExpiryDate).ToList();

        // Act
        var result = sut.SortByExpiryAndWeight(warehouse.Id);

        // // Assert
        result.Should().NotBeNull().And
            .BeEquivalentTo(expected);
    }
    
    [Fact]
    public async Task ChooseThreePalettesByExpiryAndVolume()
    {
        // Arrange
        var sut = new WarehouseQueryService(_dbContext);
        var unitOfWork = new UnitOfWork(_dbContext);
        var warehouse = _dataHelper.CreateWarehouseWithPalettesAndBoxes(
            "TestWarehouse", 7, 5);

        unitOfWork.WarehouseRepository?.InsertAsync(warehouse);
        await unitOfWork.SaveAsync();

        var expected = warehouse.Palettes?
            .OrderByDescending(p => p.ExpiryDate).Take(3)
            .OrderByDescending(p => p.Volume)
            .ToList();

        // Act
        var result = sut.ChooseThreePalettesByExpiryAndVolume(warehouse.Id);

        // // Assert
        result.Should().NotBeNull().And
            .BeEquivalentTo(expected);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _dbContext.DisposeAsync();
}