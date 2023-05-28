using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.IntegrationTests.Abstract;

namespace Wms.Web.Api.IntegrationTests.Wms.WarehouseControllerTests;

public sealed class GetAllWarehouseControllerTests : TestControllerBase
{
    private readonly IWarehouseClient _sut;

    public GetAllWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WarehouseClient(HttpClient);
    }
    
    /*
    
    [Fact(DisplayName = "GetAllWarehouses")]
    public async Task GetAll_ShouldReturnWarehouses()
    {
        // Arrange
        var warehouseId1 = Guid.NewGuid();
        var warehouseId2 = Guid.NewGuid();
        
        // Act
        var existingWarehouses = await HttpClient
            .GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>($"{Ver1}warehouses");
        
        var createFirst = await DataHelper.GenerateWarehouse(warehouseId1);
        var createdFirst = await createFirst.Content.ReadFromJsonAsync<WarehouseResponse>();
        
        var createSecond = await DataHelper.GenerateWarehouse(warehouseId2);
        var createdSecond = await createSecond.Content.ReadFromJsonAsync<WarehouseResponse>();

        var responseAll = 
            await _sut.GetAllAsync(existingWarehouses?.Count, 2, CancellationToken.None);
        
        var responseOne =
            await _sut.GetAllAsync(existingWarehouses?.Count + 1, 1, CancellationToken.None);
        
        // Assert
        createFirst.StatusCode.Should().Be(HttpStatusCode.Created);
        createSecond.StatusCode.Should().Be(HttpStatusCode.Created);
        responseAll?.Count.Should().Be(2);
        responseOne?.Count.Should().Be(1);
        responseAll!.FirstOrDefault().Should().BeEquivalentTo(createdFirst);
        responseAll!.LastOrDefault().Should().BeEquivalentTo(createdSecond);
        responseOne!.Single().Should().BeEquivalentTo(createdSecond);
    }

    [Fact(DisplayName = "GetDeletedWarehouses")]
    public async Task GetAllDeleted_ShouldReturnWarehouses()
    {
        // Arrange
        var warehouseId1 = Guid.NewGuid();
        var warehouseId2 = Guid.NewGuid();
        
        // Act
        var existingWarehouses = await HttpClient
            .GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>($"/api/v1/warehouses/archive");

        var createFirst = await HttpClient.PostAsJsonAsync(
            $"/api/v1/warehouses?warehouseId={warehouseId1}", 
            new WarehouseRequest(){Name = "Warehouse#GetDeleted1"}, CancellationToken.None);
            
        var createdFirst = await createFirst.Content.ReadFromJsonAsync<WarehouseResponse>();

        var deleteFirst = await HttpClient.DeleteAsync(
            $"/api/v1/warehouses/{warehouseId1}", CancellationToken.None);
        
        var createSecond = await HttpClient.PostAsJsonAsync(
            $"/api/v1/warehouses?warehouseId={warehouseId2}", 
            new WarehouseRequest(){Name = "Warehouse#GetDeleted2"}, CancellationToken.None);
        
        var createdSecond = await createSecond.Content.ReadFromJsonAsync<WarehouseResponse>();
        
        var deleteSecond = await HttpClient.DeleteAsync(
            $"/api/v1/warehouses/{warehouseId2}", CancellationToken.None);

        var responseAll = 
            await _sut.GetAllDeletedAsync(existingWarehouses?.Count, 2, CancellationToken.None);
        
        var responseOne =
            await _sut.GetAllDeletedAsync(existingWarehouses?.Count + 1, 1, CancellationToken.None);
        
        // Assert
        createFirst.StatusCode.Should().Be(HttpStatusCode.Created);
        createSecond.StatusCode.Should().Be(HttpStatusCode.Created);
        deleteFirst.StatusCode.Should().Be(HttpStatusCode.OK);
        deleteSecond.StatusCode.Should().Be(HttpStatusCode.OK);
        responseAll?.Count.Should().Be(2);
        responseOne?.Count.Should().Be(1);
        responseAll!.FirstOrDefault().Should().BeEquivalentTo(createdFirst);
        responseAll!.LastOrDefault().Should().BeEquivalentTo(createdSecond);
        responseOne!.Single().Should().BeEquivalentTo(createdSecond);
    }*/
}
