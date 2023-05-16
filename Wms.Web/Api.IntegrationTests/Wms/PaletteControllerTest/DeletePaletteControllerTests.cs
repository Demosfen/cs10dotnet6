using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Wms.Web.Api.IntegrationTests.Extensions;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms.PaletteControllerTest;

public sealed class DeletePaletteControllerTests : TestControllerBase
{
    private readonly PaletteClient _sut;
    private readonly WmsDataHelper _dataHelper;
    private const string BaseUri = "http://localhost";

    public DeletePaletteControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri(BaseUri)
        });
        
        _sut = new PaletteClient(HttpClient, options);
        
        _dataHelper = new WmsDataHelper(apiFactory);
    }
    
    [Fact(DisplayName = "DeleteExistingPalette")]
    public async Task Delete_ReturnsOK_WhenPaletteExistAndEmpty()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var request = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };

        await _dataHelper.GenerateWarehouse(warehouseId);
        await _dataHelper.GeneratePalette(warehouseId, paletteId, request);
        
        // Act
        var deleteResponse = await _sut.DeleteAsync(paletteId, CancellationToken.None);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(DisplayName = "DeleteNonExistingPalette")]
    public async Task Delete_ReturnsNotFound_WhenPaletteDoesNotExist()
    {
        // Act
        var deleteResponse = await _sut.DeleteAsync(Guid.NewGuid(), CancellationToken.None);

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
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        var boxRequest = new BoxRequest { 
            Depth = 1, Width = 1, Height = 1, Weight = 1, 
            ExpiryDate = new DateTime(2007, 1, 1) };

        await _dataHelper.GenerateWarehouse(warehouseId);
        await _dataHelper.GeneratePalette(warehouseId, paletteId, paletteRequest);
        await _dataHelper.GenerateBox(paletteId, boxId, boxRequest);

        // Act
        var deleteResponse = await _sut.DeleteAsync(paletteId, CancellationToken.None);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        var error = deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Result?.Status.Should().Be(422);
        error.Result?.Type.Should().Be("entity_not_empty");
    }
}
