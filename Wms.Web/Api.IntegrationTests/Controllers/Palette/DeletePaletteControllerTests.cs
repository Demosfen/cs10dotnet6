using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.IntegrationTests.Abstract;

namespace Wms.Web.Api.IntegrationTests.Wms.PaletteControllerTests;

public sealed class DeletePaletteControllerTests : TestControllerBase
{
    private readonly PaletteClient _sut;

    public DeletePaletteControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new PaletteClient(HttpClient);
    }
    /*
    [Fact(DisplayName = "DeleteExistingPalette")]
    public async Task Delete_ReturnsOK_WhenPaletteExistAndEmpty()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var request = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };

        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper.GeneratePalette(warehouseId, paletteId, request);
        
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

        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper.GeneratePalette(warehouseId, paletteId, paletteRequest);
        await DataHelper.GenerateBox(paletteId, boxId, boxRequest);

        // Act
        var deleteResponse = await _sut.DeleteAsync(paletteId, CancellationToken.None);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        var error = deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.Result?.Status.Should().Be(422);
        error.Result?.Type.Should().Be("entity_not_empty");
    }*/
}
