using FluentAssertions;
using WMS.ASP.Store.Entities;
using WMS.ASP.Tests.Abstract;
using WMS.ASP.Tests.Infrastructure;

namespace WMS.ASP.Tests.Entities;

[Collection(DbTestCollection.Name)]
public class BoxTests : WarehouseTestsBase
{
    private string WarehouseName => "Warehouse_" + Guid.NewGuid();

    public BoxTests(TestDatabaseFixture fixture)
        : base(fixture)
    {
    }
    
    [Fact(DisplayName = "Box Expiry equals Production plus 100 days by default")]
    public async Task BoxExpiry_ShouldBeEqual_ProductionPlusHundred()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 1, 0);
        
        var box = new Box(warehouse.Palettes[0].Id, 
            1, 1, 1, 10, 
            productionDate: new DateTime(2008, 1, 1));

        // Act
        var result = new DateTime(2008, 1, 1).AddDays(100);

        // Assert
        box.ExpiryDate.Should().Be(result);
    }

    [Fact(DisplayName = "Box size cannot be negative and should throw ArgumentException")]
    public async Task BoxSize_ShouldNotBe_Negative()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 1, 0);

        // Act
        Action box = () => new Box(warehouse.Palettes[0].Id, 
            -1, -1, -1, 10, 
            new DateTime(2008, 1, 1));

        // Assert
        box.Should().Throw<ArgumentException>();
    }
    
    [Fact(DisplayName = "Box with negative weight should throw ArgumenException")]
    public async Task BoxWeight_ShouldNotBe_Negative()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 1, 0);

        // Act
        Action box = () => new Box(warehouse.Palettes[0].Id, 1, 1, 1, 
            -10, 
            new DateTime(2008, 1, 1));

        // Assert
        box.Should().Throw<ArgumentException>();
    }
    
    [Fact(DisplayName = "Box ExpiryDate throw ArgumentException if it is lower than ProductionDate")]
    public async Task BoxExpiry_ShouldNotBeLowerThan_ProductionDate()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 1, 0);

        // Act
        Action box = () => new Box(warehouse.Palettes[0].Id,
            1, 1, 1, 10, 
            new DateTime(2008, 1, 1),
            new DateTime(2007, 1,1));

        // Assert
        box.Should().Throw<ArgumentException>();
    }
    
    [Fact(DisplayName = "Box ExpiryDate and ProductionDate should not be equal zero simultaneously")]
    public async Task BoxExpiryAndProduction_ShouldNotBeEmpty()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 1, 0);

        // Act
        Action box = () => new Box(warehouse.Palettes[0].Id,
            1, 1, 1, 10);

        // Assert
        box.Should().Throw<ArgumentException>();
    }
}