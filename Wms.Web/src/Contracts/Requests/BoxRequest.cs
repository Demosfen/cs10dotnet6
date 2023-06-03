namespace Wms.Web.Contracts.Requests;

public class BoxRequest
{
    public required decimal Width { get; set; }
    
    public required decimal Height { get; set; }

    public required decimal Depth { get; set; }
    
    public required decimal Weight { get; set; }

    public DateTime? ProductionDate { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
}