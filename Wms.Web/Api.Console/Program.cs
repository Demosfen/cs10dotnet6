using Wms.Web.Api.Client.Extensions;
using Wms.Web.Api.Client.Custom;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection()
    .AddConfiguration()
    .AddCustomWmsClient();

var serviceProvider = serviceCollection.BuildServiceProvider();

var client = serviceProvider.GetRequiredService<WmsClient>();

var result = await client.PostAsync(Guid.NewGuid(), "Warehouse#11", CancellationToken.None);

Console.ReadKey();

