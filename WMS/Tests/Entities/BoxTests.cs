using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using WMS.Common.Exceptions;
using WMS.Repositories.Concrete;
using WMS.Store.Entities;
using WMS.Store.Specifications;
using WMS.Tests.Abstract;
using WMS.Tests.Infrastructure;

namespace WMS.Tests.Entities;

[Collection(DbTestCollection.Name)]
public class BoxTests : WarehouseTestsBase
{
    private string WarehouseName => "Warehouse_" + Guid.NewGuid();

    public BoxTests(TestDatabaseFixture fixture)
        : base(fixture)
    {
    }

    [Fact(DisplayName = "Box size cannot be negative")]
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
}