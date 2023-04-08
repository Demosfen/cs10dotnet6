using Wms.Web.Api.Contracts.Requests;

namespace Wms.Web.Api.Contracts.Requests;

public sealed class PaletteRequest
{
    public const decimal DefaultWeight = 30;
    
    public required Guid Id { get; set; }

    public required Guid WarehouseId { get; set; }

    public WarehouseRequest? Warehouse { get; set; }

    public required decimal Width { get; set; }
    
    public decimal Height { get; set; }

    public decimal Depth { get; set; }
    
    public decimal Weight { get; set; }
    
    public decimal Volume { get; set; }

    public List<BoxRequest>? Boxes { get; set; } = new();

    public DateTime? ExpiryDate { get; set; }

    public PaletteRequest(
        Guid id,
        Guid warehouseId,
        decimal width, 
        decimal height, 
        decimal depth)
    {
        Id = id;
        WarehouseId = warehouseId;
        Width = width;
        Height = height;
        Depth = depth;
    }
}