namespace Wms.Web.Api.Contracts.Requests;

public sealed class UpdatePaletteRequest
{
    public required Guid Id { get; init; }

    public required Guid WarehouseId { get; init; }

    public required decimal Width { get; set; }
    
    public required decimal Height { get; set; }

    public required decimal Depth { get; set; }

    public required decimal Weight { get; set; }
    
    public required decimal Volume { get; set; }

    public DateTime? ExpiryDate { get; set; }
    
    public List<UpdateBoxRequest>? Boxes { get; set; } = new();
}