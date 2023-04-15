namespace Wms.Web.Api.Contracts.Requests;

public class CreateBoxRequest
{
    public required Guid PaletteId { get; set; }
    
    public required decimal Width { get; init; }
    
    public required decimal Height { get; init; }

    public required decimal Depth { get; init; }
    
    public required decimal Weight { get; init; }

    public DateTime? ProductionDate { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
}