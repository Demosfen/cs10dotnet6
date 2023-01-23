using AutoFixture;
using WMS.Data;
using FluentAssertions;
using WMS.Services.Concrete;
using Xunit;

namespace WMS.Tests;

public class WarehouseServiceTests
{
    private readonly Warehouse _sut = new Warehouse();
    
    [Fact]
    public async void Repository_ShouldSaveAndReturnWarehouse_WhenCall()
    {
        // Arrange
        var fixture = new Fixture();
        
        fixture.Customizations.Add(
            new RandomNumericSequenceGenerator(0, 10, 1));

        var box1 = fixture.Create<Box>();
        var palette1 = fixture.Create<Palette>();
        var sut = fixture.Create<Warehouse>();
        
        palette1.AddBox(box1);
        sut.AddPalette(palette1);

        // Act
        WarehouseRepository repository = new();
        
        await repository.Save(sut, "TestWarehouse.json").ConfigureAwait(false);

        var result = await repository.Read("TestWarehouse.json").ConfigureAwait(false);
        
        // Assert
        result.Should().BeSameAs(sut);
    }
}