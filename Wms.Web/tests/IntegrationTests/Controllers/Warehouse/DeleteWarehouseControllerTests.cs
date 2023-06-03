using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Warehouse;

public sealed class DeleteWarehouseControllerTests : TestControllerBase
{
    public DeleteWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
    }

    [Fact(DisplayName = "DeleteExistingWarehouse")]
    public async Task Delete_ReturnsOK_WhenWarehouseExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        await GenerateWarehouse(id);
        
        // Act
        var deleteResponse = await Sut.WarehouseClient
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
        var deletedWarehouse = await Sut.WarehouseClient
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
        var deleteResponse = await Sut.WarehouseClient
            .DeleteAsync(warehouseId);
        var error = deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.Conflict);
        error.Result?.Status.Should().Be(409);
        error.Result?.Detail.Should().Be($"The entity with id={warehouseId} not empty");
        error.Result?.Type.Should().Be("entity_not_empty");
    }
}
