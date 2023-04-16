namespace Wms.Web.Api.Contracts.Responses;

public sealed class WarehouseResponse
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }

    public List<PaletteResponse>? Palettes { set; get; } = new();
}