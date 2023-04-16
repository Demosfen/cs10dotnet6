namespace Wms.Web.Api.Contracts.Requests;

public class UpdateWarehouseRequest
{
    public required string Name { get; set; }
    
    public List<UpdatePaletteRequest>? Palettes { get; set; }
}