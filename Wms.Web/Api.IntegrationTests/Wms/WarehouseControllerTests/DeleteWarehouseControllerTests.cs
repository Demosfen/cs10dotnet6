using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Wms.Web.Api.IntegrationTests.Extensions;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms.WarehouseControllerTests;

public sealed class DeleteWarehouseControllerTests : TestControllerBase
{
    private readonly IWarehouseClient _sut;

    public DeleteWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri(BaseUri)
        });
        
        _sut = new WarehouseClient(HttpClient, options);
    }
    
    [Fact(DisplayName = "DeleteExistingWarehouse")]
    public async Task Delete_ReturnsOK_WhenWarehouseExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        await DataHelper.GenerateWarehouse(id);
        
        // Act
        var deleteResponse = await _sut.DeleteAsync(id);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(DisplayName = "DeleteNonExistWarehouse")]
    public async Task Delete_ReturnsNotFound_WhenWarehouseDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        await DataHelper.GenerateWarehouse(id);
        
        // Act
        var deleteResponse = await _sut.DeleteAsync(Guid.NewGuid());

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var error = deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Result?.Status.Should().Be(404);
        error.Result?.Title.Should().Be("The entity with specified id was not found");
        error.Result?.Type.Should().Be("entity_not_found");
    }
    
    [Fact(DisplayName = "DeleteNonEmptyWarehouse")]
    public async Task Delete_ReturnsUnprocessable_WhenWarehouseNonEmpty()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var requestPalette = new PaletteRequest()
        {
            Width = 10,
            Height = 10,
            Depth = 10
        };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper.GeneratePalette(warehouseId, paletteId, requestPalette);
        
        // Act
        var deleteResponse = await _sut.DeleteAsync(warehouseId);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        var error = deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Result?.Status.Should().Be(422);
        error.Result?.Type.Should().Be("entity_not_empty");
    }
}
