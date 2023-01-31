using FluentAssertions;
using WMS.Data;
using Xunit;

using static WMS.Tests.TestDataHelper;

namespace WMS.Tests;

public class PaletteTests
{
   [Fact]
    public void AddBox_ShouldAddBoxToThePalette()
    {
        // Arrange
        var sut = GetPalette(PaletteSample.Palette10X10X10);
        
        // Act
        sut.AddBox(GetBox(BoxSample.Box1X1X1));
        sut.AddBox(GetBox(BoxSample.Box5X5X5));
        sut.AddBox(GetBox(BoxSample.Box10X10X10));
        
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
        sut.AddBox(GetBox(BoxSample.Box1X1X1));
        sut.AddBox(GetBox(BoxSample.Box5X5X5));
        sut.AddBox(GetBox(BoxSample.Box10X10X10));
        
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
        sut.AddBox(GetBox(BoxSample.Box1X1X1));
        sut.AddBox(GetBox(BoxSample.Box5X5X5));
        
        // Assert
        sut.Weight.Should().Be(expected);
    }
    /*
    [Fact]
    public void PaletteVolume_ShouldBeSumOfBoxesVolume_PlusDefault()
    {
        // Arrange
        var sut = BigPalette;
        
        var expected = SmallBox.Volume + MediumBox.Volume + BigBox.Volume + BigPalette.Volume;

        // Act
        sut.AddBox(SmallBox);
        sut.AddBox(MediumBox);
        sut.AddBox(BigBox);

        //Assert

        sut.Volume.Should().Be(expected);
    }
    
    [Fact]
    public void PaletteConstruct_IncorrectSize_ThrowsArgumentException()
    {
        // Arrange
        Action constructNegativeWidth = () => new Palette(-1, 0, -0.1m);

        // Assert
        constructNegativeWidth.Should()
            .Throw<ArgumentException>()
            .WithMessage("Unit size (Height, Width or Depth) shouldn't be less or equal zero!");
    }

    [Fact]
    public void AddBox_OversizeBox_ThrowsArgumentException()
    {
        // Arrange
        Action addOversizeBox = () => SmallPalette.AddBox(BigBox);

        // Assert
        addOversizeBox.Should()
            .Throw<ArgumentException>()
            .WithMessage("Box size (HxWxD) greater than palette!");
    }
    */
}