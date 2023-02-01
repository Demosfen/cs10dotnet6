using WMS.Data;
using WMS.Services.Abstract;

namespace WMS.Services.Concrete;

public class WareHouseQueryService : IWarehouseQueryService
{
    public IReadOnlyCollection<IGrouping<DateTime?, Palette>> SortByExpiryAndWeight(Warehouse warehouse) => 
        warehouse.Palettes
            .Where(p => p.ExpiryDate.HasValue)
            .OrderBy(p => p.ExpiryDate)
            .ThenBy(p => p.Weight)
            .GroupBy(g => g.ExpiryDate).ToList();

    public IReadOnlyCollection<Palette> ChooseThreePalettesByExpiryAndVolume(Warehouse warehouse) =>
        warehouse.Palettes
            .OrderByDescending(p => p.ExpiryDate)
            .ThenBy(p => p.Volume)
            .Take(3).ToList();
}