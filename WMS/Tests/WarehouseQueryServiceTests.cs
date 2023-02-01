using FluentAssertions;
using WMS.Data;
using WMS.Services.Concrete;
using Xunit;
using static WMS.Tests.TestDataHelper;

namespace WMS.Tests;

public class WarehouseQueryServiceTests
{

    private readonly Palette _smallPalette = GetPalette(PaletteSample.Palette1X1X1);
    private readonly Palette _mediumPalette = GetPalette(PaletteSample.Palette10X10X10);
    private readonly Palette _bigPalette = GetPalette(PaletteSample.Palette20X20X20);

    private readonly Box _smallBox = GetBox(BoxSample.Box1X1X1, new DateTime(2009,1,1));
    private readonly Box _mediumBox1 = GetBox(BoxSample.Box1X1X1, new DateTime(2010,1,1));
    private readonly Box _mediumBox2 = GetBox(BoxSample.Box5X5X5, new DateTime(2008, 6, 6));
    private readonly Box _bigBox = GetBox(BoxSample.Box10X10X10, new DateTime(2009, 1, 1));
    
    [Fact]
    public void Sort_ByExpiryAndWeight_ShouldReturnSortedPalettes()
    {
        // Arrange
        WareHouseQueryService sut = new();
        
        Warehouse warehouse = new();

        Palette[] palettes =
        {
            _smallPalette,
            _mediumPalette,
            _bigPalette
        };
        
        // Act
        _smallPalette.AddBox(_smallBox);
        _bigPalette.AddBox(_bigBox);
        
        _mediumPalette.AddBox(_mediumBox1);
        _mediumPalette.AddBox(_mediumBox2);
        
        warehouse.AddPalette(_smallPalette);
        warehouse.AddPalette(_mediumPalette);
        warehouse.AddPalette(_bigPalette);

        


        var expected = 

        // Assert
        sut.SortByExpiryAndWeight(warehouse).Should().BeSameAs(expected);



    }
}