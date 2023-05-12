using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;

using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms;

public sealed class CreteWarehouseControllerTests : TestControllerBase
{
    private readonly IWarehouseClient _sut;

    public CreteWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri("http://localhost:5000")
        });
        
        _sut = new WarehouseClient(HttpClient, options);
    }
    
    [Fact(DisplayName = "CreateWarehouse")]
    public async Task WarehouseController_CreateWarehouse_ShouldCreateWarehouse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new WarehouseRequest
        {
            Name = "Warehouse#1"
        };
        
        // Act
        var response = await _sut.PostAsync(id, request);

        // Assert
        var warehouseResponse = await response.Content.ReadFromJsonAsync<WarehouseRequest>();

        warehouseResponse.Should().BeEquivalentTo(request);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().Be($"http://localhost/api/v1/warehouses/{id}");



    } 
}
