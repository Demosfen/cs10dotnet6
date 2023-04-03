namespace Wms.Web.Api.Contracts.Responses;

public sealed class WarehouseResponse
{
    public Guid Id { get; init; }

    public string Name { get; set; }

    public List<PaletteResponse>? Palettes { set; get; } = new();

    public WarehouseResponse(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}