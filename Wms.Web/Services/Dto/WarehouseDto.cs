namespace Wms.Web.Services.Dto;

public sealed class WarehouseDto
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public List<PaletteDto>? Palettes { set; get; } = new ();
}