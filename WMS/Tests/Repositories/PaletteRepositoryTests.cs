using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Concrete;
using WMS.Store.Entities;
using WMS.Tests.Abstract;
using WMS.Tests.Infrastructure;

namespace WMS.Tests.Repositories;

public class PaletteRepositoryTests : WarehouseTestsBase
{
    private readonly PaletteRepository _sut;

    public PaletteRepositoryTests(TestDatabaseFixture fixture)
        : base(fixture)
    {
        _sut = new PaletteRepository(DbContext);
    }
    
    [Fact(DisplayName = "Check if palette repository successfully adds and saves palettes to the existing warehouse")]
    public async Task RepositoryAdd_ShouldAddPaletteToTheWarehouse()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes("Warehouse#1", 3, 1);
        
        // Act
        await _sut.AddAsync(new Palette(warehouse.Id, 10, 10, 10), default);
        await _sut.UnitOfWork.SaveChangesAsync();

        // Assert
        var result = await DbContext.Warehouses
            .FirstOrDefaultAsync(x => x.Name == "Warehouse#1");
        
        result?.Palettes.Count.Should().Be(4);
    }
    
    [Fact(DisplayName = "Check if palette repository softly removes palettes from the existing warehouse")]
    public async Task RepositoryDelete_ShouldSoftlyDeletePalette()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes("Warehouse#2", 3, 1);
        
        // Act
        await _sut.DeleteAsync(warehouse.Palettes[0].Id, default);
        await _sut.UnitOfWork.SaveChangesAsync();
        
        // Assert
        var warehouseTest = await DbContext.Warehouses
                                     .FirstOrDefaultAsync(x => x.Name == "Warehouse#2");

        var result = warehouseTest?.Palettes[0].IsDeleted; 
        
        result.Should().Be(true);
    }
    
    [Fact(DisplayName = "Check if IsDeleted palettes is filtered")]
    public async Task SoftlyDeletedPalettes_ShouldNotBeInQueryResults()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes("Warehouse#2", 3, 1);
        
        // Act
        await _sut.DeleteAsync(warehouse.Palettes[0].Id, default);
        await _sut.UnitOfWork.SaveChangesAsync();
        
        // Assert
        var warehouseTest = await DbContext.Warehouses
            .FirstOrDefaultAsync(x => x.Name == "Warehouse#2");

        var result = warehouseTest?.Palettes.Count; 
        
        result.Should().Be(2);
    }
    
    [Fact(DisplayName = "Check if repository successfully adds box to the existing palette")]
    public async Task AddBoxes_ShouldStoreBoxesToPalette()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes("Warehouse#4", 5, 3);

        // Act
        await _sut.AddBox(warehouse.Palettes[4].Id,
            await CreateBoxAsync(warehouse.Palettes[4].Id),
            default);

        // Assert
        var result = DbContext.Warehouses
            .FirstOrDefaultAsync(x => x.Name == "Warehouse#4")
            .Result?.Palettes[4].Boxes.Count;
        
        result.Should().Be(4);
    }

    [Fact(DisplayName = "Check OversizeException")]
    public async Task OversizedBox_ShouldThrowOversizeException()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes("Warehouse#5", 5, 5);
        
        
    }
}