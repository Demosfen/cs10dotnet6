namespace Wms.Web.Api.Contracts.Requests;

public sealed class CreatePaletteRequest
{
    public required Guid Id { get; set; }

    public required Guid WarehouseId { get; set; }

    public required decimal Width { get; set; }
    
    public decimal Height { get; set; }

    public required decimal Depth { get; set; }
    
    public decimal Weight { get; set; }
    
    public decimal Volume { get; set; }

    public List<BoxRequest>? Boxes { get; set; } = new();

    public DateTime? ExpiryDate { get; set; }
}