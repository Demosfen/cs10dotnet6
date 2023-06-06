namespace Wms.Web.Business.Dto;

public sealed class WarehouseDto
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public List<PaletteDto> Palettes { set; get; } = new();
}