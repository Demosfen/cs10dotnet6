namespace Wms.Web.Api.Contracts.Responses;

public sealed class PaletteResponse
{
    public required Guid Id { get; init; }

    public required Guid WarehouseId { get; set; }

    public required decimal Width { get; init; }
    
    public required decimal Height { get; init; }

    public required decimal Depth { get; init; }

    public required decimal Weight { get; set; }
    
    public required decimal Volume { get; set; }

    public List<BoxResponse>? Boxes { get; set; } = new();

    public DateTime? ExpiryDate { get; set; }
}