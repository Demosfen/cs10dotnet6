using System.ComponentModel;
using WMS.Data;

namespace WMS.Tests;

public static class TestDataHelper
{
    public const string JsonFileName = "TestWarehouse.json";
    public static readonly DateTime DefaultProductionDate = new DateTime(2008, 1, 1);

    public enum BoxSample
    {
        Box1X1X1,
        Box5X5X5,
        Box10X10X10,
        Box20X20X20
    }

    public enum PaletteSample
    {
        Palette1X1X1,         
        Palette5X5X5,
        Palette10X10X10,
        Palette20X20X20
    }
    
    public static Box GetBox(BoxSample boxSample, DateTime? expiryDateTime = null)
    {
        return boxSample switch
        {
            BoxSample.Box1X1X1 => new Box(1, 1, 1, 1, DefaultProductionDate, expiryDateTime),
            BoxSample.Box5X5X5 => new Box(5, 5, 5, 5, DefaultProductionDate, expiryDateTime),
            BoxSample.Box10X10X10 => new Box(10, 10, 10, 10, DefaultProductionDate, expiryDateTime),
            BoxSample.Box20X20X20 => new Box(20, 20, 20, 20, DefaultProductionDate, expiryDateTime),
            _ => throw new InvalidEnumArgumentException("Incorrect box sample!")
        };
    }
    
    public static Palette GetPalette(PaletteSample paletteSample)
    {
        return paletteSample switch
        {
            PaletteSample.Palette1X1X1 => new Palette(1, 1, 1),
            PaletteSample.Palette5X5X5 => new Palette(5, 5, 5),
            PaletteSample.Palette10X10X10 => new Palette(10, 10, 10),
            PaletteSample.Palette20X20X20 => new Palette(20, 20, 20),
            _ => throw new ArgumentException("Incorrect palette sample!")
        };
    }
}