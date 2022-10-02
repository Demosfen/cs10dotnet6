using static System.Console;

try
{
    checked
    {
        int max = 500;
        for (byte i = 0; i < max; i++)
        {
            WriteLine(i);
        }
    }
}
catch (OverflowException)
{
    WriteLine("I've got OverFlow exception!");
}