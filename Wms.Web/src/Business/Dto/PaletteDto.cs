namespace Wms.Web.Business.Dto;

public sealed class PaletteDto
{
    public required Guid Id { get; init; }

    public required Guid WarehouseId { get; init; }

    public required decimal Width { get; init; }
    
    public required decimal Height { get; init; }

    public required decimal Depth { get; init; }

    public required decimal Weight { get; set; }
    
    public required decimal Volume { get; set; }

    public DateTime? ExpiryDate { get; set; }
    
    public List<BoxDto> Boxes { get; set; } = new();
}