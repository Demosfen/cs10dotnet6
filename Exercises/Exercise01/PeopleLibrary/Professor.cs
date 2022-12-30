using static System.Console;

namespace People.Library;
public class Professor: Person
{
    public Professor(string Name) : base(Name) {}

    public override void SayHello()
    {
        WriteLine($"Hello! My name is professor {Name}");
    }
    public void Explain()
    {
        WriteLine($"Listen to me now! I'm your teacher!");
    }
}
