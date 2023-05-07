using Wms.Web.Api.Client.Custom.Concrete;
using PaletteClient = Wms.Web.Api.Client.Implementations.PaletteClient;

namespace Wms.Web.Api.Client.Custom.Abstract;

public interface IWmsClient
{
    IWarehouseClient WarehouseClient { get; }
    
    IPaletteClient PaletteClient { get; }
    
    // IBoxClient BoxClient { get; }
}