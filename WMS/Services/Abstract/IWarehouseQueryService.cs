using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Services.Abstract;

public interface IWarehouseQueryService
{
    IReadOnlyCollection<IGrouping<DateTime?, Palette>> SortByExpiryAndWeight(Guid id);

    IReadOnlyCollection<Palette>? ChooseThreePalettesByExpiryAndVolume(Guid id);
}