using FluentAssertions;
using WMS.Services.Concrete;

namespace WMS.Tests;

[Collection(DbTestCollection.Name)]
public class WarehouseServiceTests: WarehouseTestsBase
{
    private readonly WarehouseQueryService _sut;
    public WarehouseServiceTests(TestDatabaseFixture fixture) : base(fixture)
    {
        _sut = new WarehouseQueryService(DbContext);
    }

    [Fact(DisplayName = "Sort by expiry date and weight")]
    public async Task<Task> SortByExpiryAndWeight_ReturnsGroupOfPalettes()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(
            "TestWarehouse", 5, 5);

        var expected = warehouse.Palettes
            .Where(p => p.ExpiryDate.HasValue)
            .OrderBy(p => p.ExpiryDate)
            .ThenBy(p => p.Weight)
            .GroupBy(g => g.ExpiryDate).ToList();

        // Act
        var result = _sut.SortByExpiryAndWeight(warehouse.Id);

        // // Assert
        result.Should().NotBeNull().And
            .BeEquivalentTo(expected);
        
        return Task.CompletedTask;
    }
    
    [Fact(DisplayName = "Choose three palettes with maximum expiry and sorted by volume")]
    public async Task ChooseThreePalettesByExpiryAndVolume()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(
            "TestWarehouse", 7, 5);

        var expected = warehouse.Palettes
            .OrderByDescending(p => p.ExpiryDate).Take(3)
            .OrderByDescending(p => p.Volume)
            .ToList();

        // Act
        var result = _sut.ChooseThreePalettesByExpiryAndVolume(warehouse.Id);

        // // Assert
        result.Should().NotBeNull().And
            .BeEquivalentTo(expected);
    }
}