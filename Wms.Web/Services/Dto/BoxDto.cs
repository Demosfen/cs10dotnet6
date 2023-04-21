namespace Wms.Web.Services.Dto;

public sealed class BoxDto
{
    public required Guid Id { get; set; }
    
    public required Guid PaletteId { get; init; }

    public required decimal Width { get; set; }
    
    public required decimal Height { get; set; }

    public required decimal Depth { get; set; }
    
    public required decimal Weight { get; set; }
    
    public decimal Volume { get; set; }

    public DateTime? ProductionDate { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
    
}