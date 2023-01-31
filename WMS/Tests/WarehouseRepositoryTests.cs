using AutoFixture;
using WMS.Data;
using FluentAssertions;
using WMS.Services.Concrete;
using Xunit;

using static WMS.Tests.TestDataHelper;

namespace WMS.Tests;

public class WarehouseRepositoryTests
{
    [Fact]
    public async void Repository_ShouldSaveAndReturnWarehouse()
    {
        // Arrange
        var smallPalette = GetPalette(PaletteSample.Palette1X1X1);
        var mediumPalette = GetPalette(PaletteSample.Palette5X5X5);
        var largePalette = GetPalette(PaletteSample.Palette10X10X10);
        
        Warehouse warehouse = new ();
        
        WarehouseRepository sut = new();
        
        // Act
        smallPalette.AddBox(GetBox(BoxSample.Box1X1X1));
        
        mediumPalette.AddBox(GetBox(BoxSample.Box1X1X1));
        mediumPalette.AddBox(GetBox(BoxSample.Box5X5X5));
        
        largePalette.AddBox(GetBox(BoxSample.Box1X1X1));
        largePalette.AddBox(GetBox(BoxSample.Box5X5X5));
        largePalette.AddBox(GetBox(BoxSample.Box10X10X10));
        
        warehouse.AddPalette(smallPalette);
        warehouse.AddPalette(mediumPalette);
        warehouse.AddPalette(largePalette);

        await sut.Save(warehouse, JsonFileName).ConfigureAwait(false);

        var result = await sut.Read(JsonFileName).ConfigureAwait(false);
        
        // Assert
        result.Should().BeEquivalentTo(warehouse);
    }
}