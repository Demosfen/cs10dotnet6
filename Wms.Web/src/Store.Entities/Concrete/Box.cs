namespace Wms.Web.Store.Entities.Concrete;

/// <summary>
/// Box class
/// </summary>
public sealed class Box : StorageUnit
{
    /// <summary>
    /// ID of the palette where box is stored
    /// </summary>
    public required Guid PaletteId { get; init; }

    /// <summary>
    /// Box weight
    /// </summary>
    public required decimal Weight { get; set; }

    /// <summary>
    /// Box production date/time
    /// </summary>
    public DateTime? ProductionDate { get; set; }
    
    /// <inheritdoc />
    public override DateTime? ExpiryDate { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"Box ID: {Id},\n" +
               $"Size (WxHxD): {Width}x{Height}x{Depth},\n" +
               $"Volume: {Volume},\n" +
               $"Weight: {Weight},\n" +
               $"Production date: {ProductionDate},\n" +
               $"Expiry date: {ExpiryDate}.\n";
    }
}