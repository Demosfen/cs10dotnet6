using WMS.Data;

namespace WMS.Services.Helpers;

public class WarehouseHelper
{
    private static readonly Warehouse DemoWarehouse = new Warehouse();

    public static Warehouse GetWarehouse()
    {
        DemoWarehouse.AddPalette(PaletteHelper.Palettes[0]);
        Console.WriteLine(PaletteHelper.Palettes[0]);
        DemoWarehouse.AddPalette(PaletteHelper.Palettes[1]);
        return DemoWarehouse;
    }
}