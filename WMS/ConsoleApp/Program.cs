using WMS.Data;
using static System.Console;
using static System.DateTime;

Warehouse warehouse = new();
Palette palette1 = new (
    10,10,10,10);

warehouse.Palettes.Add(palette1);
WriteLine(palette1.Production);

