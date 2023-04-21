using Wms.Web.Store.Interfaces;

namespace Wms.Web.Store.Entities;

public sealed class Warehouse : IEntityWithId, IAuditableEntity
{
    public required Guid Id { get; init; } 

    /// <summary>
    /// A Warehouse name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Palettes list
    /// </summary>
    public List<Palette> Palettes { get; set; } = new();


    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }

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