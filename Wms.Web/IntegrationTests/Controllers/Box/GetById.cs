using System.Net;
using FluentAssertions;
using Wms.Web.Client.Custom.Abstract;
using Wms.Web.Client.Custom.Concrete;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Box;

public sealed class GetById : TestControllerBase
{
    private readonly IWmsClient _sut;

    public GetById(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));
    }
    [Fact(DisplayName = "GetBoxById")]
    public async Task GetById_ReturnBox_WhenBoxExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        var box = await GenerateBox(paletteId, boxId);
        
        // Act
        var response = await _sut.BoxClient.GetByIdAsync(boxId, CancellationToken.None);
        
        // Assert
        response.Should().BeEquivalentTo(box);
    }
    
    [Fact(DisplayName = "NotFoundWhenBoxDoesNotExist")]
    public async Task GetById_ReturnsNotFound_WhenBoxDoesNotExist()
    {
        // Act
        async Task Act() => await _sut.BoxClient.GetByIdAsync(Guid.NewGuid());
        var exception = await Assert.ThrowsAsync<HttpRequestException>(Act);

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact(DisplayName = "GetBoxByIdIfDeleted")]
    public async Task GetById_ReturnBox_WhenBoxWasDeleted()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        var box = await GenerateBox(paletteId, boxId);
        
        await DeleteBox(boxId);
        
        // Act
        var response = await _sut.BoxClient.GetByIdAsync(boxId);

        // Assert
        response.Should().BeEquivalentTo(box);
    }
}