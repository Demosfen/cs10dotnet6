using FluentAssertions;
using WMS.Data;
using Xunit;
using static WMS.Tests.HelperObjects;

namespace WMS.Tests;

public class PaletteTests
{
   [Fact]
    public void AddBox_ShouldAddBoxToThePalette()
    {
        // Arrange
        var sut = BigPalette;
        
        // Act
        sut.AddBox(SmallBox);
        sut.AddBox(MediumBox);
        sut.AddBox(BigBox);
        
        // Assert
        sut.Boxes.Count
            .Should().Be(3);
    }

    [Fact]
    public void PaletteExpiry_ShouldEqual_MinimalBoxExpiry()
    {
        // Arrange
        var sut = BigPalette;
        DateTime? expected = SmallBox.ExpiryDate; //TODO check if it is correct?
        
        // Act
        sut.AddBox(SmallBox);
        sut.AddBox(MediumBox);
        sut.AddBox(BigBox);
        
        // Assert
        sut.ExpiryDate.Should().Be(expected);
    }

    [Fact]
    public void PaletteWeight_ShouldBeSumOfBoxes()
    {
        // Arrange
        var sut = MediumPalette;
        
        // Act
        sut.AddBox(SmallBox);
        sut.AddBox(MediumBox);

        var expected = SmallBox.Weight + MediumBox.Weight + Palette.DefaultWeight;

        sut.Weight.Should().Be(expected);
    }
    
    [Fact]
    public void PaletteConstruct_IncorrectSize_ThrowsArgumentException()
    {
        // Arrange
        Action constructNegativeWidth = () => new Palette(-1, 0, -0.1m);

        // Act

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
        
        // Act
        
        // Assert
        addOversizeBox.Should()
            .Throw<ArgumentException>()
            .WithMessage("Box size (HxWxD) greater than palette!");
    }
}