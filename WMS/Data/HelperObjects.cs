namespace WMS.Data;

public static class HelperObjects
{
    public const string JsonFileName = "TestWarehouse.json";

    public static readonly Box SmallBox = new (
        1,1,1,1,
        new DateTime(2008,10,01));
    
    public static readonly Box MediumBox = new (
        5, 5, 5, 5,
        new DateTime(2009, 01, 01),
        new DateTime(2010, 01, 01));
        
    public static readonly Box BigBox = new (
        20, 20, 20, 20,
        new DateTime(2007, 01, 01));

    public static readonly Palette SmallPalette = new (1, 1, 1);

    public static Palette MediumPalette = new (5, 5, 5);

    public static Palette BigPalette = new(20, 20, 20);
}