using static System.Console;
using ClimbingStairs.Common;

Stairs stairs1 = new();
int stair = 6;

WriteLine($"Stairs consisting of {stair} has {stairs1.ClimbingStairs(stair)} climbing methods.");