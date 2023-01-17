using System.Xml.Serialization; // XmlSerializer
using Packt.Shared; // Person
using static System.Console;
using static System.Environment;
using static System.IO.Path;


// create an object graph
List<Person> people = new()
{
    new(30000M)
    {
        FirstName = "Alice",
        LastName = "Smith",
        DateOfBirth = new(1974, 3, 14)
    },
    new(40000M)
    {
        FirstName = "Bob",
        LastName = "Jones",
        DateOfBirth = new(1969, 11, 23)
    },
    new(20000M)
    {
        FirstName = "Charlie",
        LastName = "Cox",
        DateOfBirth = new(1984, 5, 4),
        Children = new()
        {
            new(0M)
            {
                FirstName = "Sally",
                LastName = "Cox",
                DateOfBirth = new(2000, 7, 12)
            }
        }
    }
};
// create object that will format a List of Persons as XML
XmlSerializer xs = new(people.GetType());
// create a file to write to
string path = Combine(CurrentDirectory, "people.xml");
using (FileStream stream = File.Create(path))
{
// serialize the object graph to the stream
    xs.Serialize(stream, people);
}
WriteLine("Written {0:N0} bytes of XML to {1}",
    arg0: new FileInfo(path).Length,
    arg1: path);
WriteLine();
// Display the serialized object graph
WriteLine(File.ReadAllText(path));

using (FileStream xmlLoad = File.Open(path, FileMode.Open))
{
// deserialize and cast the object graph into a List of Person
if (xs.Deserialize(xmlLoad) is List<Person> loadedPeople)
    {
        foreach (Person p in loadedPeople)
        {
            WriteLine("{0} has {1} children.",
                p.LastName, p.Children?.Count ?? 0);
        }
    }
}

namespace Packt.Shared
{
    public class Person
    {
        public Person(){}
        
        public Person(decimal initialSalary)
        {
            Salary = initialSalary;
        }
    
        [XmlAttribute("fname")]
        public string? FirstName { get; set; }
        
        [XmlAttribute("lname")]
        public string? LastName { get; set; }
        
        [XmlAttribute("dob")]
        public DateTime DateOfBirth { get; set; }
        public HashSet<Person>? Children { get; set; }
        protected decimal Salary { get; set; }

    }
}