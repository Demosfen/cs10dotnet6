using WMS.Data;

namespace WMS.Tests;

public static class TestDataHelper
{
    public const string JsonFileName = "TestWarehouse.json";
    
    [System.Flags]
    public enum BoxSamples : byte
    {
        None            = 0b_0000_0000,
        Box1X1X1        = 0b_0000_0001,
        Box5X5X5        = 0b_0000_0010,
        Box10X10X10     = 0b_0000_0100
    }
    
    [System.Flags]
    public enum PaletteSamples : byte
    {
        None            = 0b_0000_0000,
        Palette1X1X1        = 0b_0000_0001,
        Palette5X5X5        = 0b_0000_0010,
        Palette10X10X10     = 0b_0000_0100
    }

    public static Box GetBox(sample, DateTime)
    {
        return  new(1, 1, 1, 10,
            new DateTime(2010, 10, 01));
    }
    
    public static Palette GetPalette()
    {
        switch (sample,)
        {
            
        }
        
        return  new(1, 1, 1, 10,
            new DateTime(2010, 10, 01));
    }
}