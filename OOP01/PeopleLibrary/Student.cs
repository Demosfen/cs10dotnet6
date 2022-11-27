using static System.Console;

namespace People.Library;

public class Student: Person
{
    public Student(string initialName) : base(initialName) {}

    public void Study()
    {
        WriteLine($"I am studying!");
    }

    public void SayAge()
    {
        WriteLine($"I'm {age} years old");
    }
}