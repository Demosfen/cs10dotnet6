using WMS.Data;
using static System.Console;

namespace WMS.ConsoleApp;

internal class Program
{
    public static void Main(string[] args)
    {
        Warehouse warehouse = new();
        Palette palette1 = new (
            10,10,10);

        Box box1 = new(
            10, 1, 10, 5,
            new DateTime(2008,1,1));
        Box box2 = new(
            1, 1, 1, 3,
            new DateTime(2009,1,1));    

        palette1.AddBox(box1);
        palette1.AddBox(box1);
        palette1.AddBox(box2);

        WriteLine(palette1.ToString());
        
        palette1.DeleteBox(box1);
        palette1.DeleteBox(box2);

        WriteLine(palette1.ToString());
    }
}