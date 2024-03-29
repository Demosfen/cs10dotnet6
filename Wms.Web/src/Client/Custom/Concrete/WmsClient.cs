using Wms.Web.Client.Custom.Abstract;

namespace Wms.Web.Client.Custom.Concrete;

internal sealed class WmsClient : IWmsClient
{
    public IWarehouseClient WarehouseClient { get; }
    public IPaletteClient PaletteClient { get; }
    public IBoxClient BoxClient { get; }

    public WmsClient(
        IWarehouseClient warehouseClient, 
        IPaletteClient paletteClient,
        IBoxClient boxClient)
    {
        WarehouseClient = warehouseClient;
        PaletteClient = paletteClient;
        BoxClient = boxClient;
    }
}