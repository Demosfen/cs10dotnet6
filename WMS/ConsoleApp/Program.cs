using WMS.Data;
using static System.Console;

Warehouse warehouse = new();
Palette palette1 = new(10,10,10,10);
Palette palette2 = new(10,10,10,10);
Box box1 = new(1,1,1,1);

palette1.Boxes!.Add(new Box(1, 1, 1, 1));
palette1.Boxes.Add(new Box(2, 1, 1, 1));

WriteLine(palette1.Boxes[0].Id);
WriteLine(palette1.Boxes[1].Id);