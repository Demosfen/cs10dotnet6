using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Services.Abstract;

public interface IWarehouseQueryService
{
    IReadOnlyCollection<IGrouping<DateTime?, Palette>> SortByExpiryAndWeight(IWarehouseDbContext dbContext, Guid id);

    IReadOnlyCollection<Palette>? ChooseThreePalettesByExpiryAndVolume(IWarehouseDbContext dbContext, Guid id);
}