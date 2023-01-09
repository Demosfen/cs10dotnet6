using WMS.Data;
using static System.Console;
using static System.DateTime;

Warehouse warehouse = new();
Palette palette1 = new (
    10,10,10,10, 
    new DateTime(2008,1,1),
    expiryDays: 120);

warehouse.Palettes.Add(palette1);
WriteLine(palette1.ProductionDate);
WriteLine(palette1.ExpiryDate);
WriteLine(palette1.Weight);
WriteLine(palette1.Volume);


