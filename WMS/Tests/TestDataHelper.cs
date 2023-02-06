using System.ComponentModel;
using WMS.Services.Concrete;
using WMS.Store.Entities;

namespace WMS.Tests;

public static class TestDataHelper
{
    public const string JsonFileName = "TestWarehouse.json";
    public static readonly DateTime DefaultProductionDate = new (2008, 1, 1);
    
    private static readonly Palette SmallPalette = GetPalette(PaletteSample.Palette1X1X1);
    private static readonly Palette MediumPalette = GetPalette(PaletteSample.Palette10X10X10);
    private static readonly Palette BigPalette = GetPalette(PaletteSample.Palette20X20X20);

    private static readonly Box SmallBox = GetBox(BoxSample.Box1X1X1, new DateTime(2009,1,1));
    private static readonly Box MediumBox1 = GetBox(BoxSample.Box1X1X1, new DateTime(2010,1,1));
    private static readonly Box MediumBox2 = GetBox(BoxSample.Box5X5X5, new DateTime(2008, 6, 6));
    private static readonly Box BigBox = GetBox(BoxSample.Box10X10X10, new DateTime(2009, 1, 1));
    
    private static readonly Warehouse warehouse = new();

    private static PaletteRepository _paletteRepository = new();

    private static WarehouseRepository _warehouseRepository = new();

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

    public static IEnumerable<object[]> SortTestData()
    {
        _paletteRepository.AddBox(SmallPalette, SmallBox); 
        _paletteRepository.AddBox(BigPalette, BigBox);

        _paletteRepository.AddBox(MediumPalette, MediumBox1);
        _paletteRepository.AddBox(MediumPalette, MediumBox2);

        _warehouseRepository.AddPalette(warehouse, SmallPalette);
        _warehouseRepository.AddPalette(warehouse, MediumPalette);
        _warehouseRepository.AddPalette(warehouse, BigPalette);

        IReadOnlyCollection<Palette> palettes = new List<Palette>()
        {
            SmallPalette,
            MediumPalette,
            BigPalette
        };

        yield return new object[] { warehouse, palettes };
    }

}