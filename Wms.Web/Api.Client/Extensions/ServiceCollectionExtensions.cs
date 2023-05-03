using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Api.Client.Custom;

namespace Wms.Web.Api.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection AddCustomWmsClient(this ServiceCollection serviceCollection)
    {
        // serviceCollection.AddSingleton<IWmsClient, WmsClient>();
        serviceCollection.AddHttpClient<WmsClient>("Wms", (provider, client) =>
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