using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Api.Console.Clients;

namespace Wms.Web.Api.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection AddWmsClient(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IWmsClient, WmsClient>();
        serviceCollection.AddHttpClient<WmsClient>("wms", (provider, client) =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            client.BaseAddress = config.GetSection("WmsApi:HostUri").Get<Uri>();
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