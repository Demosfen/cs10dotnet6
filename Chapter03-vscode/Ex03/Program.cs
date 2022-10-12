using static System.Console;

float a = 0;
int b = 3;
int c = 5;
int d = 15;

string message;
/*
while (a < 101)
{
    a++;

    message = a switch
    {
        % 15 == 0
            => "FizzBuzz ",
        % 5 == 0
            => "Buzz ",
        % 3 == 0
            => "Fizz ",
        _ => $"{a.ToString()} "
    };
    WriteLine(message);
}
*/

while (a < 101)
{
    a++;
    if (a % d == 0)
    {
        WriteLine("fizzbuzz \r");
    }
    else if (a % c == 0)
    {
        WriteLine("fizz \r");
    }
    else if (a % b == 0)
    {
        WriteLine("buzz \r");
    }
    else
    {
        WriteLine($"{a}  \r");
    }
}
