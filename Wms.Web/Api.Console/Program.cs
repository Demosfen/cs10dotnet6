using Wms.Web.Api.Client.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Contracts.Requests;
using PaletteRequest = Wms.Web.Api.Contracts.Requests.PaletteRequest;
using WarehouseRequest = Wms.Web.Api.Contracts.Requests.WarehouseRequest;

var serviceCollection = new ServiceCollection()
    .AddConfiguration()
    .AddCustomWmsClient();

var serviceProvider = serviceCollection.BuildServiceProvider();

var client = serviceProvider.GetRequiredService<IWmsClient>();

// var notDeletedWarehouse = await client.WarehouseClient.GetAllAsync(
//     0, 3, CancellationToken.None);
//
// var deletedWarehouse = await client.WarehouseClient.GetAllDeletedAsync(
//     0, 2, CancellationToken.None);
//
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
//
// var paletteCreateRequest = new PaletteRequest
// {
//     Width = 20,
//     Height = 20,
//     Depth = 20
// };

// var paletteCreate = await client
//     .PaletteClient.PostAsync(
//         new Guid("E01E24F8-3D86-472E-8FA9-7382F2062061"),
//         Guid.NewGuid(),
//         paletteCreateRequest,
//         CancellationToken.None);

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

Console.ReadKey();

