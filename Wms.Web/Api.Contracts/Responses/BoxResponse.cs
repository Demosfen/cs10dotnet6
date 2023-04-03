namespace Wms.Web.Api.Contracts.Responses;

public sealed class BoxResponse
{
    public Guid Id { get; init; }
    
    public Guid PaletteId { get; set; }
    
    public PaletteResponse Palette { get; set; }
    
    public decimal Width { get; set; }
    
    public decimal Height { get; set; }

    public decimal Depth { get; set; }
    
    public decimal Weight { get; set; }
    
    public decimal Volume { get; set; }

    public DateTime? ProductionDate { get; set; }
    
    public DateTime? ExpiryDate { get; set; }

    public BoxResponse(
        Guid id,
        Guid paletteId,
        PaletteResponse palette,
        decimal width,
        decimal height,
        decimal depth,
        decimal weight)
    {
        Id = id;
        PaletteId = paletteId;
        Palette = palette;
        Width = width;
        Height = height;
        Depth = depth;
        Weight = weight;
    }
}