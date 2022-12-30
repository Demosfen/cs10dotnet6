using static System.Console;

namespace People.Library;

public class Student: Person
{
    public Student(string Name) : base(Name) {}

    public void Study()
    {
        WriteLine($"I am studying!");
    }

    public void SayAge()
    {
        WriteLine($"I'm {Age} years old");
    }
}