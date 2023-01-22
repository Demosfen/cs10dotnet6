using WMS.Data;

namespace WMS.Services.Helpers;

public class PaletteHelper
{
    public static List<Palette> Palettes { get; } =
        new()
        {
            new Palette(5, 5, 5),
            new Palette(3,5,7)
        };

    public static List<Palette> GetPalettes()
    {
        Palettes[0].AddBox(BoxHelper.GetFirstBox);
        Palettes[0].AddBox(BoxHelper.GetSecondBox);
        Palettes[1].AddBox(BoxHelper.GetThirdBox);

        return Palettes;
    }
}