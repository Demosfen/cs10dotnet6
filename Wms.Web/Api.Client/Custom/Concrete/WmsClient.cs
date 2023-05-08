using Wms.Web.Api.Client.Custom.Abstract;

namespace Wms.Web.Api.Client.Custom.Concrete;

internal sealed class WmsClient : IWmsClient
{
    private readonly HttpClient _client;
    public IWarehouseClient WarehouseClient { get; }
    public IPaletteClient PaletteClient { get; }
    public IBoxClient BoxClient { get; }

    public WmsClient(
        HttpClient client, 
        WarehouseClient warehouseClient, 
        PaletteClient paletteClient,
        BoxClient boxClient)
    {
        _client = client;
        WarehouseClient = warehouseClient;
        PaletteClient = paletteClient;
        BoxClient = boxClient;
    }
}