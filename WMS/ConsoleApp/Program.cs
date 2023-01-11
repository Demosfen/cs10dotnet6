using WMS.Data;
using static System.Console;

internal class Program
{
    public static void Main(string[] args)
    {
        Warehouse warehouse = new();
        Palette palette1 = new (
            10,10,10);

        palette1.Boxes.Add(new Box(1,1,1,5,
            new DateTime(2011,1,1, 1, 1,0)));
        Box box1 = new(
            1, 1, 1, 5,
            new DateTime(2008,1,1));
        Box box2 = new(
            1, 1, 1, 3,
            new DateTime(2009,1,1));    

        palette1.Boxes.Add(box1);
        palette1.Boxes.Add(box1);
        palette1.Boxes.Add(box1);
        palette1.Boxes.Add(box1);
        palette1.Boxes.Add(box2);

        warehouse.Palettes.Add(palette1);

        WriteLine(palette1.ToString());
    }
}
