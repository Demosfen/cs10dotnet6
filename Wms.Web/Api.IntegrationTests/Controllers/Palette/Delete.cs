using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Controllers.Palette;

public sealed class Delete : TestControllerBase
{
    private readonly IWmsClient _sut;

    public Delete(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));
    }
    
    [Fact(DisplayName = "DeleteExistingPalette")]
    public async Task Delete_ReturnsOK_WhenPaletteExistAndEmpty()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        
        // Act
        var deleteResponse = await _sut.PaletteClient.DeleteAsync(paletteId, CancellationToken.None);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(DisplayName = "DeleteNonExistingPalette")]
    public async Task Delete_ReturnsNotFound_WhenPaletteDoesNotExist()
    {
        // Act
        var deleteResponse = await _sut.PaletteClient.DeleteAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact(DisplayName = "DeleteNonEmptyPalette")]
    public async Task Delete_ReturnsException_WhenPaletteNonEmpty()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        await GenerateBox(paletteId, boxId);

        // Act
        var deleteResponse = await _sut.PaletteClient.DeleteAsync(paletteId, CancellationToken.None);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.Conflict);
        var error = deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Result?.Status.Should().Be(409);
        error.Result?.Type.Should().Be("entity_not_empty");
    }
}
