using System;
using System.Console;

namespace Exercise.Library;
public class Person : Object
{
    public readonly string Name = "John";
    public int Age = null;
    public void SayHello()
    {
        return $"Hello! My name is {Name}";
    }
    public SetAge(int age)
    {
        Age = age;
    }

}
