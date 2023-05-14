using System.Collections;
using System.Net;
using System.Security.Cryptography;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Api.IntegrationTests.Abstract;

using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms;

public sealed class GetAllWarehouseControllerTests : TestControllerBase
{
    private readonly IWarehouseClient _sut;

    public GetAllWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri("http://localhost")
        });
        
        _sut = new WarehouseClient(HttpClient, options);
    }
    
    [Fact(DisplayName = "GetAllWarehouses")]
    public async Task GetAll_ShouldReturnWarehouses()
    {
        // Artrange
        var warehouseId1 = Guid.NewGuid();
        var warehouseId2 = Guid.NewGuid();
        
        // Act
        var createFirst = await HttpClient.PostAsJsonAsync(
            $"/api/v1/warehouses?warehouseId={warehouseId1}", 
                new WarehouseRequest(){Name = "Warehouse#GetAll1"}, CancellationToken.None);
            
        var createdFirst = await createFirst.Content.ReadFromJsonAsync<WarehouseResponse>();
        
        var createSecond = await HttpClient.PostAsJsonAsync(
            $"/api/v1/warehouses?warehouseId={warehouseId2}", 
            new WarehouseRequest(){Name = "Warehouse#GetAll2"}, CancellationToken.None);
        
        var createdSecond = await createSecond.Content.ReadFromJsonAsync<WarehouseResponse>();

        var responseAll = 
            await _sut.GetAllAsync(0, 2, CancellationToken.None);
        
        // var ListAll = (responseAll ?? throw new InvalidOperationException()).ToList();

        var responseOne =
            await _sut.GetAllAsync(1, 1, CancellationToken.None);
        
        // Assert
        createFirst.StatusCode.Should().Be(HttpStatusCode.Created);
        createSecond.StatusCode.Should().Be(HttpStatusCode.Created);
        responseAll?.Count.Should().Be(2);
        responseOne?.Count.Should().Be(1);
        responseAll!.FirstOrDefault().Should().BeEquivalentTo(createdFirst);
        responseAll!.LastOrDefault().Should().BeEquivalentTo(createdSecond);
        responseOne!.Single().Should().BeEquivalentTo(createdSecond);
    }

    [Fact(DisplayName = "GetAllWarehouses")]
    public async Task GetAllDeleted_ShouldReturnWarehouses()
    {
        // Artrange
        var warehouseId1 = Guid.NewGuid();
        var warehouseId2 = Guid.NewGuid();
        
        // Act
        var createFirst = await HttpClient.PostAsJsonAsync(
            $"/api/v1/warehouses?warehouseId={warehouseId1}", 
            new WarehouseRequest(){Name = "Warehouse#GetAll1"}, CancellationToken.None);
            
        var createdFirst = await createFirst.Content.ReadFromJsonAsync<WarehouseResponse>();
        
        var createSecond = await HttpClient.PostAsJsonAsync(
            $"/api/v1/warehouses?warehouseId={warehouseId2}", 
            new WarehouseRequest(){Name = "Warehouse#GetAll2"}, CancellationToken.None);
        
        var createdSecond = await createSecond.Content.ReadFromJsonAsync<WarehouseResponse>();

        var responseAll = 
            await _sut.GetAllAsync(0, 2, CancellationToken.None);
        
        // var ListAll = (responseAll ?? throw new InvalidOperationException()).ToList();

        var responseOne =
            await _sut.GetAllAsync(1, 1, CancellationToken.None);
        
        // Assert
        createFirst.StatusCode.Should().Be(HttpStatusCode.Created);
        createSecond.StatusCode.Should().Be(HttpStatusCode.Created);
        responseAll?.Count.Should().Be(2);
        responseOne?.Count.Should().Be(1);
        responseAll!.FirstOrDefault().Should().BeEquivalentTo(createdFirst);
        responseAll!.LastOrDefault().Should().BeEquivalentTo(createdSecond);
        responseOne!.Single().Should().BeEquivalentTo(createdSecond);
    }
}
