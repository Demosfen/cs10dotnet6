using WMS.ASP.Store.Interfaces;

namespace WMS.ASP.Store.Entities;

public sealed class Warehouse : IEntityWithId, ISoftDeletable
{
    public Guid Id { get; init; } 

    /// <summary>
    /// New property for Migrations which is
    /// a simple Warehouse name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Palettes list
    /// </summary>
    public List<Palette> Palettes { get; } = new();

    public bool IsDeleted { get; set; }

    /// <summary>
    /// Warehouse constructor
    /// </summary>
    /// <param name="name"></param>
    public Warehouse(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

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