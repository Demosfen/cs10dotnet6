using WMS.Data;
using WMS.Services.Concrete;
using Xunit;
using static WMS.Tests.TestDataHelper;

namespace WMS.Tests;

public class WarehouseQueryServiceTests
{
    [Fact]
    public void Sort_ByExpiryAndWeight_ShouldReturnSortedPalettes()
    {
        // Arrange
        WareHouseQueryService sut = new();
        
        Warehouse warehouse = new();

        var smallPalette = GetPalette(PaletteSample.Palette1X1X1);
        var mediumPalette1 = GetPalette(PaletteSample.Palette5X5X5);
        var mediumPalette2 = GetPalette(PaletteSample.Palette10X10X10);
        var bigPalette = GetPalette(PaletteSample.Palette20X20X20);

        var smallBox = GetBox(BoxSample.Box1X1X1, new DateTime(2009,1,1));
        var mediumBox1 = GetBox(BoxSample.Box1X1X1, new DateTime(2010,1,1));
        var mediumBox2 = GetBox(BoxSample.Box5X5X5, new DateTime(2008, 6, 6));
        var bigBox = GetBox(BoxSample.Box10X10X10, new DateTime(2009, 1, 1));
        
        // Act
        
        


    }
}