using Climbing.Lib;
using Xunit;

namespace ClimbingLibUnitTests
{
    public class ClimbingUnitTests
    {
        [Fact]
        public void StairCount4()
        {
            int stairCount = 4;
            int expected = 5;
            Stairs stairs = new();

            //act
            int actual = stairs.ClimbingStairs(stairCount);

            //assert
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
            Assert.Equal(expected, actual);
        }
    }
}