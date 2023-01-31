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
        var SmallPalette = GetPalette(PaletteSample.Palette1X1X1);
        var MediumPalette = GetPalette(PaletteSample.Palette5X5X5);
        var LargePalette = GetPalette(PaletteSample.Palette10X10X10);
        
        Warehouse warehouse = new ();
        
        WarehouseRepository sut = new();
        
        // Act
        SmallPalette.AddBox(GetBox(BoxSample.Box1X1X1));
        
        MediumPalette.AddBox(GetBox(BoxSample.Box1X1X1));
        MediumPalette.AddBox(GetBox(BoxSample.Box5X5X5));
        
        LargePalette.AddBox(GetBox(BoxSample.Box1X1X1));
        LargePalette.AddBox(GetBox(BoxSample.Box5X5X5));
        LargePalette.AddBox(GetBox(BoxSample.Box10X10X10));
        
        warehouse.AddPalette(SmallPalette);
        warehouse.AddPalette(MediumPalette);
        warehouse.AddPalette(LargePalette);

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