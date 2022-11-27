namespace People.Library;
public class Professor: Person
{
    public override string SayHello => $"Hello! My name is professor {name}";
    public string Explain => $"Listen to me now! I'm your teacher!";
}
