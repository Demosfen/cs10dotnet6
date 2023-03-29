using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WMS.ASP.Common.Exceptions;
using WMS.ASP.Repositories.Concrete;
using WMS.ASP.Store.Entities;
using WMS.ASP.Store.Specifications;
using WMS.ASP.Tests.Abstract;
using WMS.ASP.Tests.Infrastructure;

namespace WMS.ASP.Tests.Repositories;

[Collection(DbTestCollection.Name)]
public class PaletteRepositoryTests : WarehouseTestsBase
{
    private readonly PaletteRepository _sut;
    
    private string WarehouseName => "Warehouse_" + Guid.NewGuid();
    
    public PaletteRepositoryTests(TestDatabaseFixture fixture)
        : base(fixture)
    {
        _sut = new PaletteRepository(DbContext);
    }
    
    [Fact(DisplayName = "Check if palette repository successfully adds and saves palettes to the existing warehouse")]
    public async Task RepositoryAdd_ShouldAddPaletteToTheWarehouse()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 3, 1);
        
        // Act
        await _sut.AddAsync(new Palette(warehouse.Id, 10, 10, 10), default);
        await _sut.UnitOfWork.SaveChangesAsync();

        // Assert
        var result = await DbContext.Warehouses
            .FirstOrDefaultAsync(x => x.Name == warehouse.Name);
        
        result?.Palettes.Count.Should().Be(4);
    }
    
    [Fact(DisplayName = "Check if palette repository softly removes palettes from the existing warehouse")]
    public async Task RepositoryDelete_ShouldSoftlyDeletePalette()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 3, 1);
        
        // Act
        await _sut.DeleteAsync(warehouse.Palettes[0].Id, default);
        await _sut.UnitOfWork.SaveChangesAsync();
        
        // Assert
        var warehouseTest = await DbContext.Warehouses
                                     .FirstOrDefaultAsync(x => x.Name == warehouse.Name);

        var result = warehouseTest?.Palettes[0].IsDeleted; 
        
        result.Should().Be(true);
    }
    
    [Fact(DisplayName = "Check if IsDeleted palettes is filtered with NotDeleted filter")]
    public async Task SoftlyDeletedPalettes_ShouldNotBeInQueryResults()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 10, 1);
        
        // Act
        await _sut.DeleteAsync(warehouse.Palettes[0].Id, default);
        await _sut.UnitOfWork.SaveChangesAsync();
        
        // Assert
        var result = await DbContext.Palettes
            .Where(x => x.WarehouseId == warehouse.Id)
            .NotDeleted().CountAsync();

            result.Should().Be(9);
    }
    
    [Fact(DisplayName = "Check if repository successfully adds box to the existing palette")]
    public async Task AddBoxes_ShouldStoreBoxesToPalette()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 5, 3);

        // Act
        await _sut.AddBoxAsync(warehouse.Palettes[4].Id,
            await CreateBoxAsync(warehouse.Palettes[4].Id),
            default);

        // Assert
        var result = DbContext.Warehouses
            .FirstOrDefaultAsync(x => x.Name == warehouse.Name)
            .Result?.Palettes[4].Boxes.Count;
        
        result.Should().Be(4);
    }
    
    [Fact(DisplayName = "Check if repository successfully removes box from the existing palette")]
    public async Task RemoveBoxes_ShouldRemoveBoxesFromPalette()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 1, 3);

        // Act
        await _sut.DeleteBoxAsync(warehouse.Palettes[0].Id,
            warehouse.Palettes[0].Boxes[0],
            default);

        // Assert
        var result = DbContext.Warehouses
            .FirstOrDefaultAsync(x => x.Name == warehouse.Name)
            .Result?.Palettes[0].Boxes.Count;
        
        result.Should().Be(2);
    }

    [Fact(DisplayName = "Check OversizeException")]
    public async Task OversizeBox_ShouldThrowOversizeException()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 5, 5);
        
        var oversizeBox = new Box(
            warehouse.Palettes[0].Id, 100, 100, 100, 500, 
            new DateTime(2008,1,1));
        
        // Act
        Func<Task> act = async () => 
           await _sut.AddBoxAsync(warehouse.Palettes[0].Id, oversizeBox, default);

        // Assert
        await act.Should()
            .ThrowAsync<UnitOversizeException>();
    }
    
    [Fact(DisplayName = "Check if palette parameters incorrect (e.g. negative size WxHxD)")]
    public async Task IncorrectProperties_ShouldThrowArgumentException()
    {
        // Arrange
        var warehouse = await CreateWarehouse(WarehouseName);
        
        // Act
        Action incorrectPalette = () => new Palette(warehouse.Id, -100, -100, -100);
        
        // Assert
        incorrectPalette.Should()
            .Throw<ArgumentException>();
    }
    
    [Fact(DisplayName = "Check if repository throws exception when ExpiryData lower than Production date")]
    public async Task IncorrectExpiryAndProduction_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var warehouse = await CreateWarehouseWithPalettesAndBoxes(WarehouseName, 1, 0);
        
        // Act
        Action incorrectPalette = () => new Box(warehouse.Palettes[0].Id, 
            10, 10, 10, 10,
            new DateTime(2008,1,1),
            new DateTime(2007,1,1));
        
        // Assert
        incorrectPalette.Should()
            .Throw<ArgumentException>();
    }
}