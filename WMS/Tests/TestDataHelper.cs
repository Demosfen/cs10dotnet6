using WMS.Data;

namespace WMS.Tests;

public static class TestDataHelper
{
    public const string JsonFileName = "TestWarehouse.json";

    public static Box GetBox()
    {
        return  new(1, 1, 1, 10,
            new DateTime(2010, 10, 01));
    }

    public static Palette MediumBox()
    {
        return new(5, 5, 5, 5,
            new DateTime(2009, 01, 01));  
    }
        
    public static readonly Box BigBox = new (
        20, 20, 20, 1,
        new DateTime(2007, 01, 01));

    public static readonly Palette SmallPalette = new (1, 1, 1);

    public static Palette MediumPalette = new (5, 5, 5);

    public static Palette BigPalette = new(20, 20, 20);
}