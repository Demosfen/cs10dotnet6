using WMS.WarehouseDbContext.Interfaces;

namespace WMS.WarehouseDbContext.Entities;

public sealed record Warehouse: IEntityWithId
{
    public Guid Id { get; init; }
    public List<Palette> Palettes { get; } = new();
    
    public override string ToString()
    {
        if (Palettes.Count == 0)
        {
            return $"Warehouse contains no palettes.";
        }

        var msg = $"Warehouse contains {Palettes.Count} palettes:\n";
        
        foreach (var palette in Palettes)
        {
            msg += palette.ToString();
        }

        return msg;
    }
}