using AutoFixture;
using WMS.Data;
using FluentAssertions;
using WMS.Services.Concrete;
using Xunit;
using static WMS.Tests.HelperObjects;

namespace WMS.Tests;

public class WarehouseRepositoryTests
{
    [Fact]
    public async void Repository_ShouldSaveAndReturnWarehouse()
    {
        // Arrange
        SmallPalette.AddBox(SmallBox);
        
        MediumPalette.AddBox(SmallBox);
        MediumPalette.AddBox(MediumBox);
        
        BigPalette.AddBox(SmallBox);
        BigPalette.AddBox(MediumBox);
        BigPalette.AddBox(BigBox);
        
        warehouse.AddPalette(SmallPalette);
        warehouse.AddPalette(MediumPalette);
        warehouse.AddPalette(BigPalette);

        // Act
        WarehouseRepository _sut = new();
        
        await _sut.Save(warehouse, JsonFileName).ConfigureAwait(false);

        var result = await _sut.Read(JsonFileName).ConfigureAwait(false);
        
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