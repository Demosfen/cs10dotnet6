namespace Wms.Web.Api.Client;

public sealed class WmsClientOptions
{
    public static readonly string Wms = "Wms";

    public Uri? HostUri { get; set; }
}