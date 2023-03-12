using WMS.Store.Entities;
using WMS.Store;

namespace WMS.Services.Abstract;

public interface IWarehouseQueryService
{
    Task<List<IGrouping<DateTime?, Palette>>> SortByExpiryAndWeightAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Palette>> ChooseThreePalettesByExpiryAndVolumeAsync(Guid id, CancellationToken cancellationToken = default);
}