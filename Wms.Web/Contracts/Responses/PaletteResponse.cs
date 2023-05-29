namespace Wms.Web.Contracts.Responses;

public sealed class PaletteResponse
{
    public required Guid Id { get; init; }

    public required Guid WarehouseId { get; init; }

    public required decimal Width { get; set; }
    
    public required decimal Height { get; set; }

    public required decimal Depth { get; set; }

    public required decimal Weight { get; set; }
    
    public required decimal Volume { get; set; }

    public IReadOnlyCollection<BoxResponse> Boxes { get; set; } = Array.Empty<BoxResponse>();

    public DateTime? ExpiryDate { get; set; }
}