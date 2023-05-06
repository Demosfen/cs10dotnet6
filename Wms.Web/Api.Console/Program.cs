using Wms.Web.Api.Client.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Api.Client.Custom.Abstract;

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
//     Name = "TestingUpdate"
// };
//
// var resultUpdate = await client.WarehouseClient.PutAsync(
//     warehouseId, requestUpdate, CancellationToken.None);
//
// var resultDelete = await client.WarehouseClient.DeleteAsync(warehouseId, CancellationToken.None);

var palettesNotDeleted = await client.PaletteClient

Console.ReadKey();

