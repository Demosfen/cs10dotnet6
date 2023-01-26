using WMS.Data;
using FluentAssertions;
using Xunit;

namespace WMS.Tests;

public class BoxTests
{
    [Fact]
    public void BoxConstruct_IncorrectDates_ThrowsArgumentException()
    {
        // Arrange
        Action incorrectExpiryAndProduction = () => new Box(1,1,1, 10,
            new DateTime(2008,1,1),
            new DateTime(2007,1,1));
        
        Action noExpiryAndProduction = () => new Box(1,1,1, 10);
        
        // Act
        
        // Assert
        incorrectExpiryAndProduction.Should()
            .Throw<ArgumentException>()
            .WithMessage("Expiry date cannot be lower than Production date!");

        noExpiryAndProduction.Should()
            .Throw<ArgumentException>()
            .WithMessage("Both Production and Expiry dates shouldn't be null simultaneously");
    }
    
    [Fact]
    public void BoxConstruct_IncorrectSize_ThrowsArgumentException()
    {
        // Arrange
        Action constructNegativeWidth = () => new Box(-1,1,1, 10,
            new DateTime(2008,1,1),
            new DateTime(2007,1,1));
        
        Action constructZeroHeight = () => new Box(1,0,1, 10,
            new DateTime(2008,1,1),
            new DateTime(2007,1,1));
        
        Action constructNegativeWeight = () => new Box(1,10,0.1m, -10,
            new DateTime(2008,1,1),
            new DateTime(2007,1,1));
        
        // Act
        
        // Assert
        constructNegativeWidth.Should()
            .Throw<ArgumentException>()
            .WithMessage("Unit size (Height, Width or Depth) shouldn't be less or equal zero!");

        constructZeroHeight.Should()
            .Throw<ArgumentException>()
            .WithMessage("Unit size (Height, Width or Depth) shouldn't be less or equal zero!");
        
        constructNegativeWeight.Should()
            .Throw<ArgumentException>()
            .WithMessage("Unit weight shouldn't be less or equal zero!");
    }

    [Fact]
    public void BoxExpiry_WhenProductionOnly_GreaterAHundredDays()
    {
        // Arrange
        Box _sut = new Box(5, 5, 5, 5, new DateTime(2008, 01, 01));
        
        DateTime expected = new DateTime(2008, 1, 1).AddDays(100); //TODO change to ExpiryDays parameteer
        
        // Assert
        _sut.ExpiryDate.Should().Be(expected);
    }
}