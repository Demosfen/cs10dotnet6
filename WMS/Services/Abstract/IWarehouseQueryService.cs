using WMS.Data;

namespace WMS.Services.Abstract;

public interface IWarehouseQueryService
{
    List<Palette> SortByExpiryAndWeight();

    List<Palette> ChooseThreePalettesByExpiryAndVolume();
}