using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Responses;

namespace Wms.Web.Api.Client.Custom.Concrete;

internal sealed class WmsClient : IWmsClient
{
    private readonly HttpClient _client;
    public WarehouseClient WarehouseClient { get; }
    
    public PaletteClient PaletteClient { get; }
    
    public WmsClient(
        HttpClient client, 
        WarehouseClient warehouseClient, 
        PaletteClient paletteClient)
    {
        _client = client;
        WarehouseClient = warehouseClient;
        PaletteClient = paletteClient;
    }
}