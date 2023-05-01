using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Api.Console.Clients;

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IWmsClient, WmsClient>();
serviceCollection.AddHttpClient<WmsClient>("wms", (provider, client) =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    client.BaseAddress = config.GetSection("WmsApi:HostUri").Get<Uri>();
});

var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
IConfiguration configuration = builder.Build();
serviceCollection.AddScoped<IConfiguration>(_ => configuration);


var serviceProvider = serviceCollection.BuildServiceProvider();
var client = serviceProvider.GetRequiredService<WmsClient>();

var result = await client.PostAsync(Guid.NewGuid(), CancellationToken.None);

Console.ReadKey();