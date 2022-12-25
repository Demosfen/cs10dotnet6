using Climbing.Lib;
using Xunit;

namespace ClimbingLibUnitTests
{
    public class ClimbingUnitTests
    {
        [Fact(DisplayName = "If stairs consist of 4 stair.")]
        public void StairCount4()
        {
            int stairCount = 4;
            int expected = 5;
            Stairs stairs = new();

            //act
            int actual = stairs.ClimbingStairs(stairCount);

            //assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StairCount8()
        {
            int stairCount = 8;
            int expected = 34;
            Stairs stairs = new();

            //act
            int actual = stairs.ClimbingStairs(stairCount);

            //assert
            Assert.
            Assert.Equal(expected, actual);
        }
    }
}