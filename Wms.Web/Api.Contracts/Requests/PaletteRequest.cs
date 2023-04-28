namespace Wms.Web.Api.Contracts.Requests;

public class PaletteRequest
{
    public required decimal Width { get; set; }
    
    public required decimal Height { get; set; }

    public required decimal Depth { get; set; }
}