using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Client.Custom.Abstract;

public interface IWmsClient
{
    Task<WarehouseRequest?> PostAsync(Guid id, string name, CancellationToken cancellationToken = default);
}