using static System.Console;

/*
int x = 0;
while (x < 10)
{
    WriteLine(x);
    x++;
}
*/
int attempts = 0;
string? password;
string message;

do
{
    message = attempts switch
    {
        < 10 => $"You have {10 - attempts} attempts...",
        10 => "Last chance...",
        > 10 => "Kill yourself"
    };
    WriteLine(message);

    if (attempts > 10)
    {
        goto Kill_label;
    }
    else
    {
        Write("Enter your password: ");
        password = ReadLine();
        attempts++;
    }
}
while (password != "Pa$$w0rd");
WriteLine("Correct!");

Kill_label:
WriteLine("Stop!");