using Wms.Web.Store.Interfaces;

namespace Wms.Web.Services.Dto;

public sealed class WarehouseDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public List<PaletteDto> Palettes { get; set; } = new();
  
    public WarehouseDto(Guid id, string name, List<PaletteDto> palettes)
    {
        Id = id;
        Name = name;
        Palettes = palettes;
    }
}