using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts.Requests;

public sealed class WarehouseRequest
{
    public Guid Id { get; init; }

    public string Name { get; set; }

    public List<PaletteRequest>? Palettes { set; get; } = new();

    public WarehouseRequest(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}