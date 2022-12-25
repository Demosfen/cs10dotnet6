using static System.Console;

using Climbing.Lib;

Stairs stairs1 = new();
int stair = 6;

WriteLine($"Stairs consistiong of {stair} has {stairs1.ClimbingStairs(stair)} of climbing.");