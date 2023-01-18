using System.Text.Json.Serialization;

namespace WMS.Data;

/// <summary>
/// A class which describes a warehouse.
/// </summary>
public sealed class Warehouse
{ 
    /// <summary>
    /// Palettes stored in warehouse
    /// </summary>
    public List <Palette> Palettes { get; } = new();
}