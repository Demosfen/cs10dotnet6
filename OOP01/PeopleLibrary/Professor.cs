using static System.Console;

namespace People.Library;
public class Professor: Person
{
    public Professor(string initialName) : base(initialName) {}

    public override void SayHello()
    {
        WriteLine($"Hello! My name is professor {name}");
    }
    public void Explain()
    {
        WriteLine($"Listen to me now! I'm your teacher!");
    }
}
