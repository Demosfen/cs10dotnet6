using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Client;
using Wms.Web.Client.Custom.Abstract;
using Wms.Web.Client.Extensions;
using Wms.Web.Contracts.Requests;

var serviceCollection = new ServiceCollection();

var configuration = GetConfiguration();
serviceCollection.AddScoped<IConfiguration>(_ => configuration);

serviceCollection.Configure<WmsClientOptions>(configuration.GetSection("WmsClient"));

serviceCollection.AddCustomWmsClient();

var serviceProvider = serviceCollection.BuildServiceProvider();

var client = serviceProvider.GetRequiredService<IWmsClient>();

var warehouseId = Guid.NewGuid();

var requestCreate = new WarehouseRequest
{
    Name = warehouseId.ToString()
};

var resultCreate = await client.WarehouseClient.CreateAsync(warehouseId, requestCreate, CancellationToken.None);

var requestUpdate = new WarehouseRequest
{
    Name = "UPD-" + warehouseId
};

var resultUpdate = await client.WarehouseClient.PutAsync(
    warehouseId, requestUpdate, CancellationToken.None);

var notDeletedWarehouse = await client.WarehouseClient.GetAllAsync(
    0, 3, CancellationToken.None);

var paletteCreateRequest = new PaletteRequest
{
    Width = 10,
    Height = 10,
    Depth = 10
};

var paletteId = Guid.NewGuid();

var paletteCreate = await client
    .PaletteClient
    .CreateAsync(
        warehouseId,
        paletteId,
        paletteCreateRequest,
        CancellationToken.None);

var palettesNotDeleted = await client
    .PaletteClient.GetAllAsync(
        warehouseId,
        0,1,
        CancellationToken.None);

var paletteUpdateRequest = new PaletteRequest
{
    Width = 20,
    Height = 20,
    Depth = 20
};

var updateResult = await client.PaletteClient
    .PutAsync(
    warehouseId,
    paletteId,
    paletteUpdateRequest,
    CancellationToken.None);

var boxId = Guid.NewGuid();

var boxCreateRequest = new BoxRequest
{
    Width = 20,
    Height = 20,
    Depth = 20,
    Weight = 10,
    ExpiryDate = new DateTime(2013,01,01)
};

var boxCreate = await client.BoxClient
    .CreateAsync(
        paletteId,
        boxId,
        boxCreateRequest,
        CancellationToken.None);

var boxGet = await client.BoxClient.GetAllAsync(
    paletteId, 0, 5, cancellationToken: CancellationToken.None);

var boxUpdateRequest = new BoxRequest
{
    Width = 1,
    Height = 1,
    Depth = 1,
    Weight = 1,
    ExpiryDate = new DateTime(2010,1,1),
    ProductionDate = new DateTime(2008,1,1)
};

var boxUpdate = await client.BoxClient.PutAsync(
    boxId,
    paletteId,
    boxUpdateRequest,
    CancellationToken.None);

var boxDelete = await client.BoxClient.DeleteAsync(
    boxId,
    CancellationToken.None);

var boxGetDeleted = await client.BoxClient.GetAllDeletedAsync(
    paletteId, 0, 5, cancellationToken: CancellationToken.None);

var deletePalette = await client.PaletteClient.DeleteAsync(paletteId, CancellationToken.None);

var palettesDeleted = await client
    .PaletteClient.GetAllAsync(
        warehouseId,
        0,2,
        CancellationToken.None);

var warehouseDelete = await client.WarehouseClient.DeleteAsync(warehouseId, CancellationToken.None);

var deletedWarehouse = await client.WarehouseClient.GetAllDeletedAsync(
    0, 2, CancellationToken.None);

Console.ReadKey();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");

    return builder.Build();
}