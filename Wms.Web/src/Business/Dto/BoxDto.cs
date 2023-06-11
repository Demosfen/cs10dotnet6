namespace Wms.Web.Business.Dto;

public sealed class BoxDto
{
    public required Guid Id { get; init; }
    
    public required Guid PaletteId { get; init; }

    public required decimal Width { get; init; }
    
    public required decimal Height { get; init; }

    public required decimal Depth { get; init; }
    
    public required decimal Weight { get; init; }
    
    public decimal Volume { get; set; }

    public DateTime? ProductionDate { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
    
    public override string ToString()
    {
        return $"ID: {Id},\n" +
               $"Palette ID: {PaletteId}, \n" +
               $"Size (WxHxD): {Width}x{Height}x{Depth},\n" +
               $"Volume: {Volume},\n" +
               $"Weight: {Weight},\n" +
               $"Production date: {ProductionDate},\n" +
               $"Expiry date: {ExpiryDate}.\n";
    }
}