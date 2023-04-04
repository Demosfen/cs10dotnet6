using Wms.Web.Common.Exceptions;
using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Dto;

public sealed class PaletteDto
{
    public const decimal DefaultWeight = 30;
    
    public Guid Id { get; set; }

    public Guid WarehouseId { get; set; }

    public WarehouseDto? Warehouse { get; set; }

    public decimal Width { get; set; }
    
    public decimal Height { get; set; }

    public decimal Depth { get; set; }
    
    public decimal Weight { get; set; }
    
    public decimal Volume { get; set; }

    public List<BoxDto>? Boxes { get; set; } = new();

    public DateTime? ExpiryDate { get; set; }

    public PaletteDto(
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