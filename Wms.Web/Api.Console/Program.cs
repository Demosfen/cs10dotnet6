using Microsoft.Extensions.Configuration;
using Wms.Web.Api.Client.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Requests;

var serviceCollection = new ServiceCollection();

var configuration = GetConfiguration();
serviceCollection.AddScoped<IConfiguration>(_ => configuration);

serviceCollection.Configure<WmsClientOptions>(configuration.GetSection("WmsClient"));

serviceCollection.AddCustomWmsClient();

var serviceProvider = serviceCollection.BuildServiceProvider();

var client = serviceProvider.GetRequiredService<IWmsClient>();
//
// var notDeletedWarehouse = await client.WarehouseClient.GetAllAsync(
//     0, 3, CancellationToken.None);
//
// var deletedWarehouse = await client.WarehouseClient.GetAllDeletedAsync(
//     0, 2, CancellationToken.None);

// var warehouseId = Guid.NewGuid();
//
// var requestCreate = new WarehouseRequest
// {
//     Name = "Test_Warehouse"
// };
//
// var resultCreate = await client.WarehouseClient.PostAsync(warehouseId, requestCreate, CancellationToken.None);
//
// var requestUpdate = new WarehouseRequest
// {
//     Name = "TestingUpdate#2"
// };
//
// var resultUpdate = await client.WarehouseClient.PutAsync(
//     warehouseId, requestUpdate, CancellationToken.None);
//
// var resultDelete = await client.WarehouseClient.DeleteAsync(warehouseId, CancellationToken.None);
//
// var palettesNotDeleted = await client
//     .PaletteClient.GetAllAsync(
//         new Guid("FF1D4274-2DBB-4B03-9085-E542F314AC22"),
//         0,1,
//         CancellationToken.None);
//
// var palettesDeleted = await client
//     .PaletteClient.GetAllAsync(
//         new Guid("04F9CB58-CE3E-4BAE-92CD-C63B0EC35104"),
//         0,10,
//         CancellationToken.None);

// var paletteCreateRequest = new PaletteRequest
// {
//     Width = 20,
//     Height = 20,
//     Depth = 20
// };
//
// var paletteCreate = await client
//     .PaletteClient.PostAsync(
//         new Guid("E01E24F8-3D86-472E-8FA9-7382F2062061"),
//         Guid.NewGuid(),
//         paletteCreateRequest,
//         CancellationToken.None);
//
// var paletteUpdateRequest = new PaletteRequest
// {
//     Width = 10,
//     Height = 10,
//     Depth = 10
// };
//
// var updateResult = await client.PaletteClient.PutAsync(
//     new Guid("44F2B87A-B19A-4D7C-B875-136DCC0A3F74"),
//     new Guid("E01E24F8-3D86-472E-8FA9-7382F2062061"),
//     paletteUpdateRequest,
//     CancellationToken.None);
//
// var deleteResult = await client.PaletteClient.DeleteAsync(
//     new Guid("44F2B87A-B19A-4D7C-B875-136DCC0A3F74"), CancellationToken.None);
//
var boxCreateRequest = new BoxRequest
{
    Width = 20,
    Height = 20,
    Depth = 20,
    Weight = 10,
    ExpiryDate = new DateTime(2013,01,01)
};

var boxCreate = await client
    .BoxClient.CreateAsync(
        new Guid("E01E24F8-3D86-472E-8FA9-7382F2062061"),
        Guid.NewGuid(),
        boxCreateRequest,
        CancellationToken.None);
//
// var boxGet = await client.BoxClient.GetAllAsync(
//     new Guid("E01E24F8-3D86-472E-8FA9-7382F2062061"), 0, 5, cancellationToken: CancellationToken.None);
//
// var boxGetDeleted = await client.BoxClient.GetAllDeletedAsync(
//     new Guid("DD57D940-D495-40E7-86C2-68C4FD995CF7"), 0, 5, cancellationToken: CancellationToken.None);
//
// var boxUpdateRequest = new BoxRequest
// {
//     Width = 1,
//     Height = 1,
//     Depth = 1,
//     Weight = 1,
//     ExpiryDate = new DateTime(2010,1,1),
//     ProductionDate = new DateTime(2008,1,1)
// };
//
// var boxUpdate = await client.BoxClient.PutAsync(
//     new Guid("AEB54D44-D55C-406A-B21A-59C4BCF4CD55"),
//     new Guid("E01E24F8-3D86-472E-8FA9-7382F2062061"),
//     boxUpdateRequest,
//     CancellationToken.None);
//
// var boxDelete = await client.BoxClient.DeleteAsync(
//     new Guid("BE4FB725-D653-4E7A-8693-106363B7E7E4"),
//     CancellationToken.None);

Console.ReadKey();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");

    return builder.Build();
}