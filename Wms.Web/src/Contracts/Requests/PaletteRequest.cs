namespace Wms.Web.Contracts.Requests;

public class PaletteRequest
{
    public required decimal Width { get; init; }
    
    public required decimal Height { get; init; }

    public required decimal Depth { get; init; }
}