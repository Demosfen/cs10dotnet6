using static System.Console;

int a = 0;
int b = 3;
int c = 5;
int d = 15;

//float[] array1 = new float[] { 0, 0, 0 };
string message;
/*
while (a < 101)
{
    a++;

    message = a.ToString() switch
    {
        % 3 == 0 => "Fizz ",
        % 5 == 0 => "Buzz ",
        % 15 == 0 => "FizzBuzz ",
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