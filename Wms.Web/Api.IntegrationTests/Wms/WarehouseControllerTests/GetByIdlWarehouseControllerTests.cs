using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Api.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms.WarehouseControllerTests;

public sealed class GetByIdWarehouseControllerTests : TestControllerBase
{
    private readonly IWarehouseClient _sut;
    private const string Ver1 = "/api/v1/";

    public GetByIdWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri("http://localhost:5000")
        });
        
        _sut = new WarehouseClient(HttpClient, options);
    }
    
    [Fact(DisplayName = "GetWarehouseById")]
    public async Task GetById_ShouldReturnWarehouse()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();

        // Act
        var createWarehouse = await HttpClient.PostAsJsonAsync(
            $"{Ver1}warehouses?warehouseId={warehouseId}", 
                new WarehouseRequest(){Name = "Warehouse#GetById"}, CancellationToken.None);
            
        var createPalette = await HttpClient.PostAsJsonAsync(
            $"{Ver1}warehouses/{warehouseId}/palettes/{paletteId}", CancellationToken.None);
        var createdPalette = await createPalette.Content.ReadFromJsonAsync<PaletteResponse>();
        
        var createdWarehouse = await createWarehouse.Content.ReadFromJsonAsync<WarehouseResponse>();
        // createdWarehouse!.Palettes.Append<PaletteRequest>(createPalette);

        var response = await _sut.GetByIdAsync(warehouseId, 0, 1, CancellationToken.None);
        
        // Assert
        createWarehouse.StatusCode.Should().Be(HttpStatusCode.Created);
        createPalette.StatusCode.Should().Be(HttpStatusCode.Created);
        // responseAll?.Count.Should().Be(2);
        // responseOne?.Count.Should().Be(1);
        // responseAll!.FirstOrDefault().Should().BeEquivalentTo(createdFirst);
        // responseAll!.LastOrDefault().Should().BeEquivalentTo(createdSecond);
        // responseOne!.Single().Should().BeEquivalentTo(createdSecond);
    }
    //
    // [Fact(DisplayName = "GetDeletedWarehouses")]
    // public async Task GetAllDeleted_ShouldReturnWarehouses()
    // {
    //     // Arrange
    //     var warehouseId1 = Guid.NewGuid();
    //     var warehouseId2 = Guid.NewGuid();
    //     
    //     // Act
    //     var existingWarehouses = await HttpClient
    //         .GetFromJsonAsync<IReadOnlyCollection<WarehouseResponse>>($"/api/v1/warehouses/archive");
    //
    //     var createFirst = await HttpClient.PostAsJsonAsync(
    //         $"/api/v1/warehouses?warehouseId={warehouseId1}", 
    //         new WarehouseRequest(){Name = "Warehouse#GetDeleted1"}, CancellationToken.None);
    //         
    //     var createdFirst = await createFirst.Content.ReadFromJsonAsync<WarehouseResponse>();
    //
    //     var deleteFirst = await HttpClient.DeleteAsync(
    //         $"/api/v1/warehouses/{warehouseId1}", CancellationToken.None);
    //     
    //     var createSecond = await HttpClient.PostAsJsonAsync(
    //         $"/api/v1/warehouses?warehouseId={warehouseId2}", 
    //         new WarehouseRequest(){Name = "Warehouse#GetDeleted2"}, CancellationToken.None);
    //     
    //     var createdSecond = await createSecond.Content.ReadFromJsonAsync<WarehouseResponse>();
    //     
    //     var deleteSecond = await HttpClient.DeleteAsync(
    //         $"/api/v1/warehouses/{warehouseId2}", CancellationToken.None);
    //
    //     var responseAll = 
    //         await _sut.GetAllDeletedAsync(existingWarehouses?.Count, 2, CancellationToken.None);
    //     
    //     var responseOne =
    //         await _sut.GetAllDeletedAsync(existingWarehouses?.Count + 1, 1, CancellationToken.None);
    //     
    //     // Assert
    //     createFirst.StatusCode.Should().Be(HttpStatusCode.Created);
    //     createSecond.StatusCode.Should().Be(HttpStatusCode.Created);
    //     deleteFirst.StatusCode.Should().Be(HttpStatusCode.OK);
    //     deleteSecond.StatusCode.Should().Be(HttpStatusCode.OK);
    //     responseAll?.Count.Should().Be(2);
    //     responseOne?.Count.Should().Be(1);
    //     responseAll!.FirstOrDefault().Should().BeEquivalentTo(createdFirst);
    //     responseAll!.LastOrDefault().Should().BeEquivalentTo(createdSecond);
    //     responseOne!.Single().Should().BeEquivalentTo(createdSecond);
    // }
}
