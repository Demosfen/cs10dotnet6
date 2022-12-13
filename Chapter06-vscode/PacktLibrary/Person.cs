using static System.Console;

namespace Packt.Shared;

public class Person : object, IComparable<Person>
{
    //fields
    public string? Name;
    public DateTime DateOfBirth;
    public List<Person> Children = new();
    public event EventHandler? Shout;
    public int AngerLevel;

    //static method to multiply
    public static Person Procreate(Person p1, Person p2)
    {
        Person baby = new()
        {
            Name = $"Baby of {p1} and {p2}"
        };

        p1.Children.Add(baby);
        p2.Children.Add(baby);

        return baby;
    }

    //Instance method of procreate
    public Person ProcreateWith(Person partner)
    {
        return Procreate(this, partner);
    }

    public static Person operator *(Person p1, Person p2)
    {
        return Person.Procreate(p1, p2);
    }

    //Methods
    public void WriteToConsole()
    {
        WriteLine($"{Name} was born on a {DateOfBirth:dddd}");
    }

    public static int Factorial(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException(
                $"{nameof(number)} cannot be less than zero.");
        }

        return localFactorial(number);

        int localFactorial(int localNumber)
        {
            if (localNumber < 1) return 1;
            return localNumber * localFactorial(localNumber - 1);
        }
    }

    public void Poke()
    {
        AngerLevel++;

        if (AngerLevel >= 3)
        {

            Shout?.Invoke(this, EventArgs.Empty);
            /*           if (Shout != 0)
                       {
                           Shout(this, EventArgs.Empty);
                       }  */
        }
    }

    public int CompareTo(Person? other)
    {
        if (Name is null) return 0;
        return Name.CompareTo(other?.Name);
    }

}
