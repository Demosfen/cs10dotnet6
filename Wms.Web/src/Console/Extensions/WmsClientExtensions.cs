using Wms.Web.Client.Custom.Abstract;
using Wms.Web.Contracts.Requests;

namespace Wms.Web.Console.Extensions;

public static class WmsClientExtensions
{
    public static async Task TryWarehouseClient(
        this IWmsClient client, 
        Guid warehouseId)
    {
        var requestCreate = new WarehouseRequest
        {
            Name = warehouseId.ToString()
        };

        await client.WarehouseClient.CreateAsync(warehouseId, requestCreate, CancellationToken.None);

        var requestUpdate = new WarehouseRequest
        {
            Name = "UPD-" + warehouseId
        };

        await client.WarehouseClient.PutAsync(
            warehouseId, requestUpdate, CancellationToken.None);

        await client.WarehouseClient.GetAllAsync(
            0, 3, CancellationToken.None);
    }

    public static async Task TryPaletteClient(
        this IWmsClient client,
        Guid warehouseId,
        Guid paletteId)
    {
        var paletteCreateRequest = new PaletteRequest
        {
            Width = 10,
            Height = 10,
            Depth = 10
        };

        await client
            .PaletteClient
            .CreateAsync(
                warehouseId,
                paletteId,
                paletteCreateRequest,
                CancellationToken.None);

        await client
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

        await client.PaletteClient
            .PutAsync(
                paletteId,
                warehouseId,
                paletteUpdateRequest,
                CancellationToken.None);
    }

    public static async Task TryBoxClient(
        this IWmsClient client,
        Guid warehouseId,
        Guid paletteId,
        Guid boxId)
    {
        var boxCreateRequest = new BoxRequest
        {
            Width = 20,
            Height = 20,
            Depth = 20,
            Weight = 10,
            ExpiryDate = new DateTime(2013,01,01)
        };
        
        await client.BoxClient
            .CreateAsync(
                paletteId,
                boxId,
                boxCreateRequest,
                CancellationToken.None);
        
        await client.BoxClient.GetAllAsync(
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
        
        await client.BoxClient.PutAsync(
            boxId,
            paletteId,
            boxUpdateRequest,
            CancellationToken.None);
        
        await client.BoxClient.DeleteAsync(
            boxId,
            CancellationToken.None);
        
        await client.BoxClient.GetAllDeletedAsync(
            paletteId, 0, 5, cancellationToken: CancellationToken.None);
        
        await client.PaletteClient.DeleteAsync(paletteId, CancellationToken.None);
        
        await client
            .PaletteClient.GetAllAsync(
                warehouseId,
                0,2,
                CancellationToken.None);
        
        await client.WarehouseClient.DeleteAsync(warehouseId, CancellationToken.None);
        
        await client.WarehouseClient.GetAllDeletedAsync(
            0, 2, CancellationToken.None);
    }
}