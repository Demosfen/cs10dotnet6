using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;

namespace Wms.Web.Api.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection AddCustomWmsClient(this ServiceCollection serviceCollection)
    {
        var configureClient = new Action<IServiceProvider, HttpClient>((provider, client) =>
        {
            var config = provider.GetRequiredService<IConfiguration>();

            client.BaseAddress = config.GetRequiredSection(WmsClientOptions.Wms)
                                     .Get<WmsClientOptions>()?
                                     .HostUri
                                 ?? throw new InvalidOperationException(
                                     $"Not initiated value: {nameof(WmsClientOptions.HostUri)}");
        });

        serviceCollection.AddHttpClient<IWarehouseClient, WarehouseClient>(configureClient);
        serviceCollection.AddHttpClient<IPaletteClient, PaletteClient>(configureClient);
        serviceCollection.AddHttpClient<IBoxClient, BoxClient>(configureClient);
        serviceCollection.AddTransient<IWmsClient, WmsClient>();

        return serviceCollection;
    }
}