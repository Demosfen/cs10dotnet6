using WMS.Data;
using WMS.Services.Abstract;

namespace WMS.Services.Concrete;

public class WareHouseQueryService
{
    public static List<Palette> SortByExpiryAndWeight(Warehouse warehouse) =>
        new (warehouse.Palettes
            .Where(p => p.ExpiryDate.HasValue)
            .OrderBy(p => p.ExpiryDate) 
            .ThenBy(p => p.Weight));

    public static List<Palette> ChooseThreePalettesByExpiryAndVolume(Warehouse warehouse) =>
        new (warehouse.Palettes
            .OrderByDescending(p => p.ExpiryDate)
            .ThenBy(p => p.Volume)
            .Take(3));
}