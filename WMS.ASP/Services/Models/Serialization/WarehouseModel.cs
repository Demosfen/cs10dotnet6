using WMS.ASP.Store.Interfaces;

namespace WMS.ASP.Services.Models.Serialization;

public sealed class WarehouseModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public List<PaletteModel> Palettes { get; set; } = new();

    public WarehouseModel(List<PaletteModel> palettes, string name)
    {
        this.Palettes = palettes;
        Name = name;
        Palettes = palettes;
    }
}

