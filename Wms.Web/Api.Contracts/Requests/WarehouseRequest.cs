using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts.Requests;

public sealed class WarehouseRequest
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }

    public List<PaletteRequest>? Palettes { set; get; } = new();
}