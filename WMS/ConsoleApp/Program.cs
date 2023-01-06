using WMS.Data;
using static System.Console;
using static System.DateTime;

Warehouse warehouse = new();
Palette palette1 = new (
    10,10,10,10);

warehouse.Palettes.Add(palette1);
WriteLine(palette1.Height);
WriteLine(palette1.Id);
WriteLine(palette1.Production);
WriteLine(palette1.ExpiryDate);
WriteLine(palette1.ExpiryDays);

