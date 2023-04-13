using Wms.Web.Common.Exceptions;
using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Dto;

public sealed class PaletteDto
{
    private const int DefaultWeight = 30;
    public required Guid Id { get; set; }

    public required Guid WarehouseId { get; init; }

    public required decimal Width { get; init; }
    
    public required decimal Height { get; init; }

    public required decimal Depth { get; init; }

    public decimal Weight { get; set; } = DefaultWeight;
    
    public decimal Volume { get; set; }

    public List<BoxDto>? Boxes { get; set; } = new();

    public DateTime? ExpiryDate { get; set; }
}