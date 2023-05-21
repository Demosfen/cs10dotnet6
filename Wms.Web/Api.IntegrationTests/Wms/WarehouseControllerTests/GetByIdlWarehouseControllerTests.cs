using System.Net;
using FluentAssertions;
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

    public GetByIdWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WarehouseClient(HttpClient);
    }
    
    [Fact(DisplayName = "GetWarehouseById")]
    public async Task GetById_ReturnWarehouse_WhenWarehouseExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        
        var createWarehouse = await DataHelper.GenerateWarehouse(warehouseId);
        var createdWarehouse = await createWarehouse.Content.ReadFromJsonAsync<WarehouseResponse>();
        
        var createPalette = await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
        var createdPalette = await createPalette.Content.ReadFromJsonAsync<PaletteRequest>();

        // Act
        var response = await _sut.GetByIdAsync(warehouseId, 0, 1, CancellationToken.None);
        
        // Assert
        createWarehouse.StatusCode.Should().Be(HttpStatusCode.Created);
        createPalette.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Should().BeEquivalentTo(createdWarehouse, 
            options => options.Excluding(x => x!.Palettes));
        response?.Palettes.ElementAt(0).Should().BeEquivalentTo(createdPalette);
    }

    [Fact(DisplayName = "NotFoundWhenWarehouseDoesNotExist")]
    public async Task GetById_ReturnsNotFound_WhenWarehouseDoesNotExist()
    {
        // Act
        try
        {
            await _sut.GetByIdAsync(Guid.NewGuid(), 0, 0, CancellationToken.None);
        }
        catch (HttpRequestException response)
        {
            response.StatusCode.HasValue.Should().Be(true);
            response.StatusCode?.Should().Be(HttpStatusCode.NotFound);
        }
    }
    
    [Fact(DisplayName = "GetWarehouseByIdIfDeleted")]
    public async Task GetById_ReturnWarehouse_WhenWarehouseWasDeleted()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();

        var createWarehouse = await DataHelper.GenerateWarehouse(warehouseId);
        var createdWarehouse = await createWarehouse.Content.ReadFromJsonAsync<WarehouseResponse>();

        var deleteWarehouse = await DataHelper.DeleteWarehouse(warehouseId);
        
        // Act
        var response = await _sut.GetByIdAsync(warehouseId, 0, 0, CancellationToken.None);
        
        // Assert
        createWarehouse.StatusCode.Should().Be(HttpStatusCode.Created);
        deleteWarehouse.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().BeEquivalentTo(createdWarehouse);
    }
}
