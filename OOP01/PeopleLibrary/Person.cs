using System;
using static System.Console;
using System.Collections.Generic;

public class Person
{
    public readonly string name = "John";
    int age;
    public virtual string SayHello => $"Hello! My name is {name}";
    public void SetAge (int n)
    {
        age = n;
    }
    public int SayAge => age;

}