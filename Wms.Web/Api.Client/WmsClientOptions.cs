namespace Wms.Web.Api.Client;

public sealed class WmsClientOptions
{
    public static readonly string Wms = "Wms";
    
    public static readonly string WmsClient = "WmsClient";
    
    public Uri? HostUri { get; set; }

    public string? WarehouseClientBaseUrl { get; set; }

    public string? PaletteClientBaseUrl { get; set; }
    
    public string? BoxClientBaseUrl { get; set; }
}