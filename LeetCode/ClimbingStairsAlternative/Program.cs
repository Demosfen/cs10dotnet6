using static System.Console;

ClimbingStairs(stair: 6);

static void ClimbingStairs(int stair)
{
    int fib1 = 1, fib2 = 1;

    while (stair-- > 0)
    {
        fib2 += fib1;
        fib1 = fib2 - fib1;
    }

    WriteLine($"Stairs has {fib1} climbing method(s).");
}