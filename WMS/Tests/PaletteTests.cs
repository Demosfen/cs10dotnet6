using FluentAssertions;
using WMS.Data;
using Xunit;
using static WMS.Tests.HelperObjects;

namespace WMS.Tests;

public class PaletteTests
{
   [Fact]
    public void PaletteConstruct_IncorrectSize_ThrowsArgumentException()
    {
        // Arrange
        Action constructNegativeWidth = () => new Palette(-1, 1, 1);

        Action constructZeroHeight = () => new Palette(1, 0, 1);

        Action constructNegativeDepth = () => new Palette(1, 10, -0.1m);

        // Act

        // Assert
        constructNegativeWidth.Should()
            .Throw<ArgumentException>()
            .WithMessage("Unit size (Height, Width or Depth) shouldn't be less or equal zero!");

        constructZeroHeight.Should()
            .Throw<ArgumentException>()
            .WithMessage("Unit size (Height, Width or Depth) shouldn't be less or equal zero!");

        constructNegativeDepth.Should()
            .Throw<ArgumentException>()
            .WithMessage("Unit weight shouldn't be less or equal zero!");
    }

    [Fact]
    public void AddBox_OversizedBox_ThrowsArgumentException()
    {
        // Arrange
        Action addOversizedBox = () => SmallPalette.AddBox(MediumBox);
        
        // Act
        
        // Assert
        addOversizedBox.Should()
            .Throw<ArgumentException>()
            .WithMessage("Box size (HxWxD) greater than palette!");
    }
    
    [Fact]
    public void AddBox_ShouldAddBoxToThePalette()
    {
        // Arrange
        var _sut = SmallPalette;
        
        // Act
        _sut.AddBox(SmallBox);
        
        // Assert
        _sut.Boxes.Count
            .Should().Be(1);
    }

    [Fact]
    public void PaletteExpiry_ShouldEqual_MinimalBoxExpiry()
    {
        // Arrange
        var _sut = BigPalette;
        DateTime? expected = SmallBox.ExpiryDate; //TODO check if it is correct.
        
        // Act
        _sut.AddBox(SmallBox);
        _sut.AddBox(MediumBox);
        _sut.AddBox(BigBox);
        
        // Assert
        _sut.ExpiryDate.Should().Be(expected);
    }

    [Fact]
    public void PaletteWeight_ShouldBeSumOfBoxes()
    {
        // Arrange
        var _sut = MediumPalette;
        
        // Act
        _sut.AddBox(SmallBox);
        _sut.AddBox(MediumBox);

        var expected = SmallBox.Weight + MediumBox.Weight + 30; //TODO change default weight addition
    }
}