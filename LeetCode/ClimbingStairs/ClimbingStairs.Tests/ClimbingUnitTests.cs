using ClimbingStairs.Common;

namespace ClimbingStairs.Tests;

public sealed class ClimbingUnitTests
{
    [Theory(DisplayName = "...")]
    [InlineData(4, 5)]
    [InlineData(8, 34)]
    public void ClimbingStairs_ShouldCalculate(int stairsCount, int expectedResult)
    {
        Stairs stairs = new();

        //act
        int actual = stairs.ClimbingStairs(stairsCount);

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expectedResult, actual);
    }
}