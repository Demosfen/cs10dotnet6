using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;

namespace Wms.Web.Api.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection AddCustomWmsClient(this ServiceCollection serviceCollection) //TODO how not to repeat a code?
    {
        serviceCollection.AddHttpClient<WarehouseClient>((provider, client) => //TODO why I cannot use AddHttpClient<IWarehouseClient, WarehouseClient>?
            {
                var config = provider.GetRequiredService<IConfiguration>();
                
                client.BaseAddress = config.GetSection(WmsClientOptions.Wms)
                .Get<WmsClientOptions>()?
                .HostUri
                ?? throw new InvalidOperationException(
                    $"Not initiated value: {nameof(WmsClientOptions.HostUri)}");
            });
        
        serviceCollection.AddHttpClient<PaletteClient>((provider, client) =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
                
            client.BaseAddress = config.GetSection(WmsClientOptions.Wms)
                                     .Get<WmsClientOptions>()?
                                     .HostUri
                                 ?? throw new InvalidOperationException(
                                     $"Not initiated value: {nameof(WmsClientOptions.HostUri)}");
        });
        
        serviceCollection.AddHttpClient<IWmsClient, WmsClient>((provider, client) =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            
            client.BaseAddress = config.GetSection(WmsClientOptions.Wms)
                .Get<WmsClientOptions>()?
                .HostUri
                ?? throw new InvalidOperationException(
                    $"Not initiated value: {nameof(WmsClientOptions.HostUri)}");
        });

        return serviceCollection;
    }
    
    public static ServiceCollection AddConfiguration(this ServiceCollection serviceCollection)
    {
        IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }

        var configuration = GetConfiguration();
        serviceCollection.AddScoped<IConfiguration>(_ => configuration);

        return serviceCollection;
    }
}