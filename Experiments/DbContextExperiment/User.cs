namespace DbContextExperiment;

public class User
{
    public int Id { get; }
    public string? Name { get; }
    public int Age { get; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}