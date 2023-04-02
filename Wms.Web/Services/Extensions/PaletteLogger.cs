using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Extensions;

public static class PaletteExtensions
{
    /// <summary>
    /// Print all boxes on the palette
    /// to the Console
    /// </summary>
    public static void PrintAllBoxes(this Palette palette)
    {
        if (palette.Boxes.Count == 0)
        {
            Console.WriteLine("No boxes to output!");
            return;
        }

        foreach (var box in palette.Boxes)
        {
            Console.WriteLine(box.ToString());
        }
    }
}