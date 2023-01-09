using WMS.Data;
using static System.Console;

Warehouse warehouse = new();
Palette palette1 = new (
    10,10,10,10, 
    new DateTime(2008,1,1));

Box box1 = new(
    1, 1, 1, 5,
    new DateTime(2008,1,1));

palette1.Boxes?.Add(box1);

warehouse.Palettes.Add(palette1);
WriteLine(palette1.Width);
WriteLine(palette1.Height);
WriteLine(palette1.Depth);
WriteLine(palette1.ProductionDate);
WriteLine(palette1.ExpiryDate);
WriteLine(box1.Weight);
WriteLine(palette1.Volume);


