namespace Wms.Web.Api.Console.Clients;

public sealed class WmsClientOptions
{
    public static readonly string Wms = "WmsApi"; 
    public Uri? HostUri { get; set; }
}