using System;
using static System.Console;
using System.Collections.Generic;

public class Person
{
    protected int Age;
    protected readonly string Name;

    public Person(string Name)
    {
        this.Name = Name;
    }
    
    public virtual void SayHello() 
    {
        WriteLine($"Hello! My name is {Name}");
    } 

    public void SetAge (int Age)
    {
        this.Age = Age;
    }

}