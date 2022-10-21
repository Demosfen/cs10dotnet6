using static System.Console;
using System.Linq;

// a string array is a sequence that implements IEnumerable<string>
string[] names = new[] { "Michael", "Pam", "Jim", "Dwight", "Angela", "Kevin", "Toby", "Creed"};
WriteLine("Deffered execution");

var query1 = names.Where(name => name.EndsWith("m"));
var query2 = from name in names where name.EndsWith("m") select name;

string[] result1 = query1.ToArray();
List<string> result2 = query2.ToList();

foreach (string name in query1)
{
    WriteLine(name);
    names[2] = "Jimmy";
}

WriteLine("Writing queries");
//var query = names.Where(new Func<string, bool>(NameLongerThanFour));
//var query = names.Where(NameLongerThanFour);
IOrderedEnumerable<string> query = names
.Where(name => name.Length > 4)
.OrderBy(name => name.Length)
.ThenBy(name => name);

foreach (string item in query)
{
    WriteLine(item);
}

/*static bool NameLongerThanFour(string name)
{
    return name.Length > 4;
}*/

WriteLine("Filtering by type");
List<Exception> exceptions = new()
{
new ArgumentException(),
new SystemException(),
new IndexOutOfRangeException(),
new InvalidOperationException(),
new NullReferenceException(),
new InvalidCastException(),
new OverflowException(),
new DivideByZeroException(),
new ApplicationException()
};

IEnumerable<ArithmeticException> arithmeticExceptionsQuery = exceptions.OfType<ArithmeticException>();

foreach (ArithmeticException exception in arithmeticExceptionsQuery)
{
WriteLine(exception);
}