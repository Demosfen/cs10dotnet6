using WMS.Store.Entities;
using WMS.WarehouseDbContext;

namespace WMS.Services.Abstract;

public interface IWarehouseQueryService
{
    Task<List<IGrouping<DateTime?, Palette>>> SortByExpiryAndWeight(Guid id);

    Task<List<Palette>> ChooseThreePalettesByExpiryAndVolume(Guid id);
}