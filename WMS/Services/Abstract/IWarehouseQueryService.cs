using WMS.Data;

namespace WMS.Services.Abstract;

public interface IWarehouseQueryService
{
    List<Palette> SortByExpiryAndWeight(Warehouse warehouse);

    List<Palette> ChooseThreePalettesByExpiryAndVolume(Warehouse warehouse);
}