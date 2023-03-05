using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests;

public class WarehouseRepositoryTests: WarehouseTestsBase
{
    public WarehouseRepositoryTests(TestDatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact(DisplayName = "Check if UnitOfWork successfully inserts and saves entities")]
    public async Task RepositoryInsert_ShouldInsertWarehouse()
    {
        // Arrange
        await CreateWarehouseWithPalettesAndBoxes("Warehouse1", 1, 1);
        
        // Act
        var result = DbContext.Warehouses.FirstOrDefaultAsync(x => x.Name == "Warehouse1");

        // Assert

        result.Should().NotBeNull();

    }
}