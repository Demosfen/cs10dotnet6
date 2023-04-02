using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Dto;

public sealed class PaletteDto
{
    public Guid Id { get; set; }
    public decimal Width { get; set; }
    
    public decimal Height { get; set; }

    public decimal Depth { get; set; }
    public Guid WarehouseId { get; set; }

    public const decimal DefaultWeight = 30;

    public List<BoxDto>? Boxes { get; set; } = new();

    public decimal Weight { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public PaletteDto(
        Guid id,
        decimal width, 
        decimal height, 
        decimal depth,
        List<BoxDto>? boxes,
        Guid warehouseId)
    {
        Id = id;
        Width = width;
        Height = height;
        Depth = depth;
        Boxes = boxes;
        WarehouseId = warehouseId;
    }
}