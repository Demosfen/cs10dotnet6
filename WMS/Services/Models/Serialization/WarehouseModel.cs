namespace WMS.Services.Models.Serialization;

public class WarehouseModel
{
    public WarehouseModel(List<PaletteModel> palettes)
    {
        Palettes = palettes;
    }

    public List<PaletteModel> Palettes { get; }
}

