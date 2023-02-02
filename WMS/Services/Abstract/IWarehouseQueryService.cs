using WMS.Data;

namespace WMS.Services.Abstract;

public interface IWarehouseQueryService
{
    IReadOnlyCollection<IGrouping<DateTime?, Palette>> SortByExpiryAndWeight(Warehouse warehouse);

    IReadOnlyCollection<Palette> ChooseThreePalettesByExpiryAndVolume(Warehouse warehouse);
}