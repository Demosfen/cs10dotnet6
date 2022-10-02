﻿using static System.Console;

bool a = true;
bool b = false;
WriteLine($"AND | a | b ");
WriteLine($"a   | {a & a,-5} | {a & b,-5} ");
WriteLine($"b   | {b & a,-5} | {b & b,-5} ");
WriteLine();
WriteLine($"OR  | a | b ");
WriteLine($"a   | {a | a,-5} | {a | b,-5} ");
WriteLine($"b   | {b | a,-5} | {b | b,-5} ");
WriteLine();
WriteLine($"XOR | a | b ");
WriteLine($"a   | {a ^ a,-5} | {a ^ b,-5} ");
WriteLine($"b   | {b ^ a,-5} | {b ^ b,-5} ");

static bool DoStuff()
{
    WriteLine("I am doing some stuff.");
    return true;
}

WriteLine();
WriteLine($"a & DoStuff() = {a && DoStuff()}");
WriteLine($"b & DoStuff() = {b && DoStuff()}");