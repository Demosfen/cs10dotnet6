using Wms.Web.Common.Exceptions;
using Wms.Web.Store.Entities;

namespace Wms.Web.Api.Contracts.Responses;

public sealed class PaletteResponse
{
    public const decimal DefaultWeight = 30;
    
    public Guid Id { get; set; }

    public Guid WarehouseId { get; set; }

    public WarehouseResponse? Warehouse { get; set; }

    public decimal Width { get; set; }
    
    public decimal Height { get; set; }

    public decimal Depth { get; set; }
    
    public decimal Weight { get; set; }
    
    public decimal Volume { get; set; }

    public List<BoxResponse>? Boxes { get; set; } = new();

    public DateTime? ExpiryDate { get; set; }

    public PaletteResponse(
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