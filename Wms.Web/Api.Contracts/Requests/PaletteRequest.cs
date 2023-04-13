namespace Wms.Web.Api.Contracts.Requests;

public sealed class PaletteRequest
{
    public required Guid Id { get; set; }

    public required Guid WarehouseId { get; init; }

    public required decimal Width { get; init; }
    
    public required decimal Height { get; init; }

    public required decimal Depth { get; init; }

    public required decimal Weight { get; set; }
    
    public required decimal Volume { get; set; }

    public DateTime? ExpiryDate { get; set; }
    
    public List<BoxRequest>? Boxes { get; set; } = new();
}