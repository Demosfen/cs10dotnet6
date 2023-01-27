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
        var sut = BigPalette;
        
        // Act
        sut.AddBox(SmallBox);
        sut.AddBox(MediumBox);
        sut.AddBox(BigBox);
        
        // Assert
        sut.Boxes.Should()
            .NotBeEmpty().And
            .Contain(SmallBox).And
            .Contain(MediumBox).And
            .Contain(BigBox);
    }

    [Fact]
    public void PaletteExpiry_ShouldEqual_MinimalBoxExpiry()
    {
        // Arrange
        var sut = BigPalette;

        var boxes = new List<Box>();

        // Act
        sut.AddBox(SmallBox);
        sut.AddBox(MediumBox);
        sut.AddBox(BigBox);
        
        boxes.Add(SmallBox);
        boxes.Add(MediumBox);
        boxes.Add(BigBox);
        
        DateTime? expected = boxes.Min(box => box.ExpiryDate);
        
        // Assert
        sut.ExpiryDate.Should().Be(expected);
    }

    [Fact]
    public void PaletteWeight_ShouldBeSumOfBoxesWeight_PlusDefault()
    {
        // Arrange
        var sut = MediumPalette;
        
        var expected = SmallBox.Weight + MediumBox.Weight + Palette.DefaultWeight;
        
        // Act
        sut.AddBox(SmallBox);
        sut.AddBox(MediumBox);
        
        // Assert
        sut.Weight.Should().Be(expected);
    }
    
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
}