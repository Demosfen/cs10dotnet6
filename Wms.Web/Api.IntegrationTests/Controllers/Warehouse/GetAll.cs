using FluentAssertions;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Api.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Controllers.Warehouse;

public sealed class GetAll : TestControllerBase
{
    private readonly IWmsClient _sut;

    private const string Ver1 = "/api/v1/";

    public GetAll(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));
    }

    [Theory(DisplayName = "GetAllWarehouses")]
    [InlineData(0, 2, 2)]
    [InlineData(1, 1, 1)]
    public async Task GetAll_ShouldReturnWarehouses(int offset, int size, int expectedCount)
    {
        // Arrange
        var warehouseId1 = Guid.NewGuid();
        var warehouseId2 = Guid.NewGuid();
        
        // Act
        var existingWarehouses = await HttpClient
            .GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>($"{Ver1}warehouses");
        
        await GenerateWarehouse(warehouseId1);

        var createdSecond = await GenerateWarehouse(warehouseId2);

        var response =
            await _sut.WarehouseClient
                .GetAllAsync(existingWarehouses?.Count + offset, size, CancellationToken.None);

        // Assert
        response?.Count.Should().Be(expectedCount);
        response?.Should().ContainEquivalentOf(createdSecond);
    }

    [Theory(DisplayName = "GetDeletedWarehouses")]
    [InlineData(0, 2, 2)]
    [InlineData(1, 1, 1)]
    public async Task GetAllDeleted_ShouldReturnWarehouses(int offset, int size, int expectedCount)
    {
        // Arrange
        var warehouseId1 = Guid.NewGuid();
        var warehouseId2 = Guid.NewGuid();
        
        // Act
        var existingWarehouses = await HttpClient
            .GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>($"{Ver1}warehouses/archive");

        await GenerateWarehouse(warehouseId1);
        var createdSecond = await GenerateWarehouse(warehouseId2);

        await DeleteWarehouse(warehouseId1);
        await DeleteWarehouse(warehouseId2);

        var response = await _sut.WarehouseClient
            .GetAllDeletedAsync(existingWarehouses?.Count + offset, size, CancellationToken.None);
        
        // Assert
        response?.Count.Should().Be(expectedCount);
        response?.Should().ContainEquivalentOf(createdSecond);
    }
}
