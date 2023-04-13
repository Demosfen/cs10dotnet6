namespace Wms.Web.Api.Contracts.Requests;

public class UpdateWarehouseRequest
{
    public required string Name { get; set; }
    
    public List<PaletteRequest>? Palettes { get; set; }
}