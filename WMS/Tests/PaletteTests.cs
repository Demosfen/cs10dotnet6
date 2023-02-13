using FluentAssertions;
using WMS.Repositories.Concrete;
using WMS.Services.Concrete;
using WMS.WarehouseDbContext.Entities;
using Xunit;

using static WMS.Tests.TestDataHelper;

namespace WMS.Tests;

public class PaletteTests
{
    PaletteRepository paletteRepository = new();
    
   [Fact]
    public void AddBox_ShouldAddBoxToThePalette()
    {
        // Arrange
        var sut = GetPalette(PaletteSample.Palette10X10X10);
        
        // Act
        /*sut.AddBox(GetBox(BoxSample.Box1X1X1));
        sut.AddBox(GetBox(BoxSample.Box5X5X5));
        sut.AddBox(GetBox(BoxSample.Box10X10X10));*/
        
        // Assert
        sut.Boxes.Should()
            .NotBeEmpty();
    }

    [Fact]
    public void PaletteExpiry_ShouldEqual_MinimalBoxExpiry()
    {
        // Arrange
        var sut = GetPalette(PaletteSample.Palette10X10X10);

        var boxes = new List<Box>();

        // Act
        paletteRepository.AddBox(sut, GetBox(BoxSample.Box1X1X1));
        paletteRepository.AddBox(sut, GetBox(BoxSample.Box5X5X5));
        paletteRepository.AddBox(sut, GetBox(BoxSample.Box10X10X10));
        
        boxes.Add(GetBox(BoxSample.Box1X1X1));
        boxes.Add(GetBox(BoxSample.Box5X5X5));
        boxes.Add(GetBox(BoxSample.Box10X10X10));
        
        DateTime? expected = boxes.Min(box => box.ExpiryDate);
        
        // Assert
        sut.ExpiryDate.Should().Be(expected);
    }

    [Fact]
    public void PaletteWeight_ShouldBeSumOfBoxesWeight_PlusDefault()
    {
        // Arrange
        var sut = GetPalette(PaletteSample.Palette5X5X5);

        var expected = GetBox(BoxSample.Box1X1X1).Weight + GetBox(BoxSample.Box5X5X5).Weight + Palette.DefaultWeight;

        // Act
        paletteRepository.AddBox(sut, GetBox(BoxSample.Box1X1X1));
        paletteRepository.AddBox(sut, GetBox(BoxSample.Box5X5X5));

        // Assert
        sut.Weight.Should().Be(expected);
    }
}