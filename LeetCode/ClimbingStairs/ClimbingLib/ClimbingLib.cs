using System;

namespace Climbing.Lib;

public class Stairs
{
    /// <summary>
    /// Fibonacchi (n-1)th element
    /// </summary>
    private int fib1 = 1;
    /// <summary>
    /// Fibonacchi nth element
    /// </summary>
    private int fib2 = 1;
    public int ClimbingStairs(int n)
    {
        while (n-- > 0)
        {
            fib2 += fib1;
            fib1 = fib2 - fib1;
        }

        return fib1;
    }

}
