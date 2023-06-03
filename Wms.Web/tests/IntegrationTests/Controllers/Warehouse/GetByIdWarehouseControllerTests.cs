using System.Net;
using FluentAssertions;
using Wms.Web.Client.Custom.Abstract;
using Wms.Web.Client.Custom.Concrete;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Warehouse;

public sealed class GetByIdWarehouseControllerTests : TestControllerBase
{
    private readonly IWmsClient _sut;

    public GetByIdWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));
    }
    
    [Fact(DisplayName = "GetWarehouseById")]
    public async Task GetById_ReturnWarehouse_WhenWarehouseExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();

        var createdWarehouse = await GenerateWarehouse(warehouseId);

        // Act
        var response = await _sut.WarehouseClient
            .GetByIdAsync(warehouseId, 0, 1, CancellationToken.None);
        
        // Assert
        response.Should().BeEquivalentTo(createdWarehouse);
    }

    [Fact(DisplayName = "NotFoundWhenWarehouseDoesNotExist")]
    public async Task GetById_ReturnsNotFound_WhenWarehouseDoesNotExist()
    {
        // Act
        // Act
        async Task Act() => await _sut.WarehouseClient
            .GetByIdAsync(Guid.NewGuid(), 0,0, CancellationToken.None);
        
        var exception = await Assert.ThrowsAsync<HttpRequestException>(Act);
        
        // Assert
        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact(DisplayName = "GetWarehouseByIdIfDeleted")]
    public async Task GetById_ReturnWarehouse_WhenWarehouseWasDeleted()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();

        var createdWarehouse = await GenerateWarehouse(warehouseId);

        await DeleteWarehouse(warehouseId);
        
        // Act
        var response = await _sut.WarehouseClient
            .GetByIdAsync(warehouseId, 0, 0, CancellationToken.None);
        
        // Assert
        response.Should().BeEquivalentTo(createdWarehouse);
    }
}
