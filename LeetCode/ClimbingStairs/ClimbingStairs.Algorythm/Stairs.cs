namespace ClimbingStairs.Common;

public sealed class Stairs
{
    /// <summary>
    /// Fibonacci (n-1)th element
    /// </summary>
    private int _fib1 = 1;
    
    /// <summary>
    /// Fibonacci nth element
    /// </summary>
    private int _fib2 = 1;
    
    public int ClimbingStairs(int n)
    {
        while (n-- > 0)
        {
            _fib2 += _fib1;
            _fib1 = _fib2 - _fib1;
        }

        return _fib1;
    }
}
