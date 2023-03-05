using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Services.Abstract;

public interface IWarehouseQueryService
{
    Task<List<IGrouping<DateTime?, Palette>>> SortByExpiryAndWeight(Guid id);

    Task<List<Palette>> ChooseThreePalettesByExpiryAndVolume(Guid id);
}