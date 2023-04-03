using Wms.Web.Store.Interfaces;

namespace Wms.Web.Services.Dto;

public sealed class WarehouseDto
{
    public Guid Id { get; init; }

    public string Name { get; set; }

    public List<PaletteDto>? Palettes { set; get; } = new();

    public WarehouseDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}