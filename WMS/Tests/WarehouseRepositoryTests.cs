using AutoFixture;
using WMS.Data;
using FluentAssertions;
using WMS.Services.Concrete;
using Xunit;

using static WMS.Data.HelperObjects;

namespace WMS.Tests;

public class WarehouseRepositoryTests
{
    [Fact]
    public async void Repository_ShouldSaveAndReturnWarehouse()
    {
        // Arrange
        var firstPalette = SmallPalette;
        var secondPalette = MediumPalette;
        var thirdPalette = BigPalette;
        
        Warehouse warehouse = new ();
        
        WarehouseRepository sut = new();
        
        // Act
        firstPalette.AddBox(SmallBox);
        
        secondPalette.AddBox(SmallBox);
        secondPalette.AddBox(MediumBox);
        
        thirdPalette.AddBox(SmallBox);
        thirdPalette.AddBox(MediumBox);
        thirdPalette.AddBox(BigBox);
        
        warehouse.AddPalette(SmallPalette);
        warehouse.AddPalette(MediumPalette);
        warehouse.AddPalette(BigPalette);

        await sut.Save(warehouse, JsonFileName).ConfigureAwait(false);

        var result = await sut.Read(JsonFileName).ConfigureAwait(false);
        
        // Assert
        result.Should().BeEquivalentTo(warehouse);
    }
}


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