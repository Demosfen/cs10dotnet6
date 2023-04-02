using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Abstract;

public interface IWarehouseQueryService
{
    Task<List<IGrouping<DateTime?, Palette>>> SortByExpiryAndWeightAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Palette>> ChooseThreePalettesByExpiryAndVolumeAsync(Guid id, CancellationToken cancellationToken = default);
}