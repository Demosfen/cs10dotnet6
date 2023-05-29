using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Controllers.Warehouse;

public sealed class Delete : TestControllerBase
{
    private readonly IWmsClient _sut;

    public Delete(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));    }

    [Fact(DisplayName = "DeleteExistingWarehouse")]
    public async Task Delete_ReturnsOK_WhenWarehouseExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        await GenerateWarehouse(id);
        
        // Act
        var deleteResponse = await _sut.WarehouseClient
            .DeleteAsync(id);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(DisplayName = "DeleteNonExistWarehouse")]
    public async Task Delete_ReturnsNotFound_WhenWarehouseDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        await GenerateWarehouse(id);
        
        // Act
        var deletedWarehouse = await _sut.WarehouseClient
            .DeleteAsync(Guid.NewGuid());

        // Assert
        deletedWarehouse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var error = deletedWarehouse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Result?.Status.Should().Be(404);
        error.Result?.Title.Should().Be("The entity with specified id was not found");
        error.Result?.Type.Should().Be("entity_not_found");
    }
    
    [Fact(DisplayName = "DeleteNonEmptyWarehouse")]
    public async Task Delete_ReturnsConflict_WhenWarehouseNonEmpty()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        
        // Act
        var deleteResponse = await _sut.WarehouseClient
            .DeleteAsync(warehouseId);
        var error = deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.Conflict);
        error.Result?.Status.Should().Be(409);
        error.Result?.Detail.Should().Be($"The entity with id={warehouseId} not empty");
        error.Result?.Type.Should().Be("entity_not_empty");
    }
}
