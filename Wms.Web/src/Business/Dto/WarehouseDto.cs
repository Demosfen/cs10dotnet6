namespace Wms.Web.Business.Dto;

public sealed class WarehouseDto
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public List<PaletteDto> Palettes { set; get; } = new();
    
    public override string ToString()
    {
        if (Palettes is { Count: 0 })
        {
            return $"Warehouse contains no palettes.";
        }

        var msg = $"Warehouse contains {Palettes!.Count} palettes:\n";

        return Palettes.Aggregate(
            msg, (current, palette) => current + palette);
    }
}