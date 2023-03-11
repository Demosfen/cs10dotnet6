using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Concrete;
using WMS.Store.Entities;
using WMS.Tests.Abstract;
using WMS.Tests.Infrastructure;

namespace WMS.Tests.Repositories;

public class WarehouseRepositoryTests: WarehouseTestsBase
{
    private readonly WarehouseRepository _sut;

    public WarehouseRepositoryTests(TestDatabaseFixture fixture) 
        : base(fixture)
    {
        _sut = new WarehouseRepository(DbContext);
    }

    [Fact(DisplayName = "Check if repository successfully inserts and saves entities")]
    public async Task RepositoryInsert_ShouldInsertWarehouse()
    {
        // Arrange, Act
        await _sut.AddAsync(new Warehouse("Warehouse#1"), default);
        await _sut.UnitOfWork.SaveChangesAsync();

        // Assert
        var result = await DbContext.Warehouses
            .FirstOrDefaultAsync(x => x.Name == "Warehouse#1");
        
        result.Should().NotBeNull();
    }
    
    [Fact(DisplayName = "Check if repository successfully softly deletes entities")]
    public async Task RepositoryDelete_ShouldSoftDeleteWarehouse()
    {
        // Arrange
        var warehouse = new Warehouse("Warehouse2");
        
        // Act
        await DbContext.AddAsync(warehouse);
        await DbContext.SaveChangesAsync();

        await _sut.DeleteAsync(warehouse.Id, default);
        await _sut.UnitOfWork.SaveChangesAsync();

        // Assert
        var result = await DbContext.Warehouses
            .FindAsync(warehouse.Id);
        result?.IsDeleted.Should().Be(true);
    }
    
    [Fact(DisplayName = "Check if repository successfully adds palette to the warehouse")]
    public async Task AddPalettes_ShouldStorePalettesToWarehouse()
    {
        // Arrange
        var warehouse = await CreateWarehouse("Warehouse#3");
        
        // Act
        await _sut.AddPalette(warehouse.Id, new Palette(warehouse.Id, 10, 10, 10), default);

        // Assert
        var result = warehouse.Palettes.Count;
        result.Should().Be(1);
    }
}