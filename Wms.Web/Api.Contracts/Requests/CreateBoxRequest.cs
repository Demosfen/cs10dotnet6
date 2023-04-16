namespace Wms.Web.Api.Contracts.Requests;

public class CreateBoxRequest
{
    public required Guid PaletteId { get; init; }
    
    public required decimal Width { get; set; }
    
    public required decimal Height { get; set; }

    public required decimal Depth { get; set; }
    
    public required decimal Weight { get; set; }

    public DateTime? ProductionDate { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
}