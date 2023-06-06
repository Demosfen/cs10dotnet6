using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Client;
using Wms.Web.Client.Custom.Abstract;
using Wms.Web.Client.Extensions;
using Wms.Web.Console.Extensions;

var serviceCollection = new ServiceCollection();

var configuration = GetConfiguration();
serviceCollection.AddScoped<IConfiguration>(_ => configuration);

serviceCollection.Configure<WmsClientOptions>(configuration.GetSection("WmsClient"));

serviceCollection.AddCustomWmsClient();

var serviceProvider = serviceCollection.BuildServiceProvider();

var client = serviceProvider.GetRequiredService<IWmsClient>();

var warehouseId = Guid.NewGuid();
var paletteId = Guid.NewGuid();
var boxId = Guid.NewGuid();

await client.TryWarehouseClient(warehouseId);

await client.TryPaletteClient(warehouseId, paletteId);

await client.TryBoxClient(warehouseId, paletteId, boxId);

Console.ReadKey();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");

    return builder.Build();
}