using Wms.Web.Store.Interfaces;

namespace Wms.Web.Store.Entities;

public sealed class Warehouse : IEntityWithId, ISoftDeletable
{
    public required Guid Id { get; init; } 

    /// <summary>
    /// New property for Migrations which is
    /// a simple Warehouse name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Palettes list
    /// </summary>
    public List<Palette>? Palettes { get; set; } = new();

    public bool IsDeleted { get; set; }

    public override string ToString()
    {
        if (Palettes is { Count: 0 })
        {
            return $"Warehouse contains no palettes.";
        }

        var msg = $"Warehouse contains {Palettes!.Count} palettes:\n";

        return Palettes.Aggregate(
            msg, (current, palette) => current + palette.ToString());
    }
}