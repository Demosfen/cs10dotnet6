using FluentAssertions;
using Wms.Web.Contracts.Responses;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Warehouse;

public sealed class GetAllWarehouseControllerTests : TestControllerBase
{
    public GetAllWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
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
        var existingWarehouses = await Sut.WarehouseClient
            .GetAllAsync(offset, size, CancellationToken.None);
        
        await GenerateWarehouse(warehouseId1);

        var createdSecond = await GenerateWarehouse(warehouseId2);

        var response =
            await Sut.WarehouseClient
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
        var existingWarehouses = await Sut.WarehouseClient
            .GetAllAsync(offset, size, CancellationToken.None);

        await GenerateWarehouse(warehouseId1);
        var createdSecond = await GenerateWarehouse(warehouseId2);

        await DeleteWarehouse(warehouseId1);
        await DeleteWarehouse(warehouseId2);

        var response = await Sut.WarehouseClient
            .GetAllDeletedAsync(existingWarehouses?.Count + offset, size, CancellationToken.None);
        
        // Assert
        response?.Count.Should().Be(expectedCount);
        response?.Should().ContainEquivalentOf(createdSecond);
    }
}
