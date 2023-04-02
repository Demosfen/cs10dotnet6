namespace Wms.Web.Services.Dto;

public sealed class BoxDto
{
    public Guid Id { get; set; }
    
    public decimal Width { get; set; }
    
    public decimal Height { get; set; }

    public decimal Depth { get; set; }
    
    public decimal Volume { get; set; }
    
    public decimal Weight { get; set; }
    
    public DateTime? ProductionDate { get; set; }
    
    public DateTime? ExpiryDate { get; set; }
    
    public Guid PaletteId { get; set; }

    public BoxDto(Guid id,
        decimal width,
        decimal height,
        decimal depth,
        Guid paletteId,
        DateTime? expiryDate = null,
        DateTime? productionDate = null)
    {
        Id = id;
        Width = width;
        Height = height;
        Depth = depth;
        PaletteId = paletteId;
        ProductionDate = productionDate;
        ExpiryDate = expiryDate;
    }
}