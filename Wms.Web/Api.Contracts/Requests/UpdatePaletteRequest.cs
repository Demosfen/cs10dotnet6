namespace Wms.Web.Api.Contracts.Requests;

public sealed class UpdatePaletteRequest
{
    public required Guid WarehouseId { get; set; }
    
    public required decimal Width { get; set; }
    
    public required decimal Height { get; set; }

    public required decimal Depth { get; set; }
}