using System.Text.Json.Serialization;

namespace WMS.Data;

/// <summary>
/// A class which describes a warehouse.
/// </summary>
public sealed class Warehouse
{
    [JsonInclude]
    public readonly List<Palette> Palettes = new();
}