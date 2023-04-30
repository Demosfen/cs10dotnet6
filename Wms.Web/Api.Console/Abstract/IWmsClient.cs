using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Console.Clients;

public interface IWmsClient
{
    Task<WarehouseResponse?> PostAsync(
        Guid id, CancellationToken cancellationToken = default);
}