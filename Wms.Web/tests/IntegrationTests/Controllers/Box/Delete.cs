using System.Net;
using FluentAssertions;
using Wms.Web.Client.Custom.Abstract;
using Wms.Web.Client.Custom.Concrete;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Box;

public sealed class Delete : TestControllerBase
{
    private readonly IWmsClient _sut;

    public Delete(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));
    }

    [Fact(DisplayName = "DeleteExistingBox")]
    public async Task Delete_ReturnsOK_WhenBoxExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        await GenerateBox(paletteId, boxId);
        
        // Act
        var deleteResponse = await _sut.BoxClient.DeleteAsync(boxId, CancellationToken.None);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(DisplayName = "DeleteNonExistingBox")]
    public async Task Delete_ReturnsNotFound_WhenBoxDoesNotExist()
    {
        // Act
        var deleteResponse = await _sut.BoxClient.DeleteAsync(Guid.NewGuid(), CancellationToken.None);
    
        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
