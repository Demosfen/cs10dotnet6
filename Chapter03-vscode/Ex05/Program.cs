using static System.Console;

int x = 3;
int y = 2 + ++x;
WriteLine(y);

x = 3 << 2;
y = 10 >> 1;
WriteLine($"{x}\n{y}");

x = 10 & 8;
y = 10 | 7;
WriteLine($"{x}\n{y}");
