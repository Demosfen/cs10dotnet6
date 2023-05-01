using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Api.Console.Clients;
using Wms.Web.Api.Console.Extensions;

var serviceCollection = new ServiceCollection()
    .AddConfiguration()
    .AddWmsClient();

var serviceProvider = serviceCollection.BuildServiceProvider();

var client = serviceProvider.GetRequiredService<WmsClient>();

var result = await client.PostAsync(Guid.NewGuid(), CancellationToken.None);

Console.ReadKey();

