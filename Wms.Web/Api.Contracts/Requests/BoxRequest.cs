namespace Wms.Web.Api.Contracts.Requests;

public sealed class BoxRequest
{
    public required Guid Id { get; init; }
    
    public required Guid PaletteId { get; set; }
    
    public required decimal Width { get; set; }
    
    public required decimal Height { get; set; }

    public required decimal Depth { get; set; }
    
    public required decimal Weight { get; set; }
    
    public required decimal Volume { get; set; }

    public DateTime? ProductionDate { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
}