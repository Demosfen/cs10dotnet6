using AutoFixture;
using WMS.Data;
using FluentAssertions;
using WMS.Services.Concrete;
using WMS.Services.Models.Serialization;
using Xunit;

namespace WMS.Tests;

public class WarehouseRepositoryTests
{
    private readonly Warehouse _sut = new Warehouse();
    
    [Fact]
    public async void Repository_ShouldSaveAndReturnWarehouse_WhenCall()
    {
        // Arrange

        var box1 = new Box(1, 2, 3, 10,
            new DateTime(2008, 1, 1),
            new DateTime(2009, 1, 1));
        
        var box2 = new Box(3, 2, 1, 5,
            new DateTime(2009, 1, 1),
            new DateTime(2010, 1, 1));

        var palette1 = new Palette(5, 6, 7);

        var _sut = new Warehouse();
        
        palette1.AddBox(box1);
        palette1.AddBox(box2);
        _sut.AddPalette(palette1);
        
        /*
         //TODO: 1. Autofixture + Customizations if Readonly Properties; 2. DateTime/int ranges; 3. Suppress Exceptions?
        var fixture = new Fixture();
        fixture.Customize<Box>(
            x => x.With(o => o.ProductionDate, new DateTime(2008, 1, 1))
                .With(o=>o.ExpiryDate, new DateTime(2009,01,01)));

        var box1 = fixture.Create<Box>();
        var palette1 = fixture.Create<Palette>();
        var sut = fixture.Create<Warehouse>();
        
        palette1.AddBox(box1);
        sut.AddPalette(palette1);
        */

        // Act
        WarehouseRepository repository = new();
        
        await repository.Save(_sut, "TestWarehouse.json").ConfigureAwait(false);

        var result = await repository.Read("TestWarehouse.json").ConfigureAwait(false);
        
        // Assert
        result.Should().BeEquivalentTo(_sut);
    }
}