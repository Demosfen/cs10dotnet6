namespace Wms.Web.Api.Contracts.Responses;

public sealed class WarehouseResponse
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }

    public IReadOnlyCollection<PaletteResponse> Palettes { set; get; } = Array.Empty<PaletteResponse>();
}