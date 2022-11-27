using System;
using static System.Console;
using System.Collections.Generic;

public class Person
{
    protected int age;
    protected readonly string name;
    public virtual void SayHello() 
    {
        WriteLine($"Hello! My name is {name}");
    } 
    public Person(string initialName)
    {
        name = initialName;
    }
    public void SetAge (int age)
    {
        this.age = age;
    }

}