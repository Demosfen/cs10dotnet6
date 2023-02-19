using WMS.WarehouseDbContext.Interfaces;

namespace WMS.WarehouseDbContext.Entities;

public sealed record Warehouse(string Name) : IEntityWithId, ISoftDeletable
{
    public Guid Id { get; init; }

    /// <summary>
    /// New property for Migrations which is
    /// a simple Warehouse name
    /// </summary>
    public string Name { get; } = Name;

    public List<Palette> Palettes { get; } = new();

    public bool IsDeleted { get; set; }

    public override string ToString()
    {
        if (Palettes.Count == 0)
        {
            return $"Warehouse contains no palettes.";
        }

        var msg = $"Warehouse contains {Palettes.Count} palettes:\n";

        return Palettes.Aggregate(
            msg, (current, palette) => current + palette.ToString());
    }
}