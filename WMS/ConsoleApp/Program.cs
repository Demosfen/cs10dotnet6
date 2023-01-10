using WMS.Data;
using static System.Console;

Warehouse warehouse = new();
Palette palette1 = new (
    10,10,10,10, 
    new DateTime(2008,1,1));

palette1.Boxes.Add(new Box(1,1,1,5,
    new DateTime(2009,1,1, 1, 1,0)));

warehouse.Palettes.Add(palette1);
WriteLine(palette1.Width);
WriteLine(palette1.Height);
WriteLine(palette1.Depth);
WriteLine(palette1.ProductionDate);
WriteLine(palette1.ExpiryDate);
WriteLine(palette1.Boxes[0].Weight);
WriteLine(palette1.Volume);

Box box1 = new(
    1, 1, 1, 5,
    new DateTime(2008,1,1));
    
palette1.Boxes.Add(box1);
palette1.Boxes.Add(box1);
palette1.Boxes.Add(box1);
palette1.Boxes.Add(box1);

WriteLine(palette1.Boxes[2].Weight);

Box box2 = new(
    1, 1, 1, 3,
    new DateTime(2008,1,1));
    
palette1.Boxes.Add(box2);

WriteLine(palette1.Boxes[5].Weight);
WriteLine(palette1.Boxes.Count);
WriteLine(palette1.Volume);
