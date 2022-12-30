using static System.Console;

/*
int x = 0;
while (x < 10)
{
    WriteLine(x);
    x++;
}

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
*/

string[] names = { "Adam", "Barry", "Charlie" };
foreach (string name in names)
{
    WriteLine($"{name} has {name.Length} characters.");
}

/*
IEnumerator e = names.GetEnumerator();
while (e.MoveNext())
{
    string name = (string)e.Current; // Current is read-only!
    WriteLine($"{name} has {name.Length} characters.");
}
*/