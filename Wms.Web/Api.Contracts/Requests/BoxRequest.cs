namespace Wms.Web.Api.Contracts.Requests;

public sealed class BoxRequest
{
    public Guid Id { get; init; }
    
    public Guid PaletteId { get; set; }
    
    public decimal Width { get; set; }
    
    public decimal Height { get; set; }

    public decimal Depth { get; set; }
    
    public decimal Weight { get; set; }
    
    public decimal Volume { get; set; }

    public DateTime? ProductionDate { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
}