using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Console.Clients;

public interface IWmsClient
{
    Task<WarehouseRequest?> PostAsync(Guid id, CancellationToken cancellationToken = default);
}