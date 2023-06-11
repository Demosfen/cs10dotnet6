using Wms.Web.Store.Entities.Interfaces;

namespace Wms.Web.Store.Entities.Concrete;

/// <summary>
/// Warehouse class
/// </summary>
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

    /// <inheritdoc />
    public DateTime CreatedAt { get; set; }
    
    /// <inheritdoc />
    public DateTime? UpdatedAt { get; set; }
    
    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }

    /// <inheritdoc />
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