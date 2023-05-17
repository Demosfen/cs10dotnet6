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

namespace Wms.Web.Api.IntegrationTests.Wms.PaletteControllerTests;

public sealed class CretePaletteControllerTests : TestControllerBase
{
    private readonly PaletteClient _sut;
    
    public CretePaletteControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri(BaseUri)
        });
        
        _sut = new PaletteClient(HttpClient, options);
    }
    
    [Fact(DisplayName = "CreatePalette")]
    public async Task Create_ShouldCreatePalette()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var request = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        
        var createWarehouse = await DataHelper.GenerateWarehouse(warehouseId);
        
        var createPalette = await _sut
            .PostAsync(warehouseId, paletteId, request, CancellationToken.None);
        var paletteResponse = await createPalette.Content.ReadFromJsonAsync<PaletteRequest>();

        // Assert
        createWarehouse.StatusCode.Should().Be(HttpStatusCode.Created);
        createPalette.StatusCode.Should().Be(HttpStatusCode.Created);
        paletteResponse.Should().BeEquivalentTo(request);
        createPalette.Headers.Location
            .Should().Be($"{BaseUri}{Ver1}palettes/{paletteId.ToString()}");
    }
    
    [Fact(DisplayName = "CreatePaletteConflict")]
    public async Task Create_ShouldReturnConflict_IfPaletteExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var request = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        
        var createWarehouse = await DataHelper.GenerateWarehouse(warehouseId);
        
        // Act
        var createPaletteFirst = await _sut
            .PostAsync(warehouseId, paletteId, request, CancellationToken.None);
        
        var createPaletteSecond = await _sut
            .PostAsync(warehouseId, paletteId, request, CancellationToken.None);
        
        // Assert
        createWarehouse.StatusCode.Should().Be(HttpStatusCode.Created);
        createPaletteFirst.StatusCode.Should().Be(HttpStatusCode.Created);
        createPaletteSecond.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
    
    [Fact(DisplayName = "PaletteSizeValidation")]
    public async Task Create_ShouldReturnError_IfPaletteSizeIncorrect()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var request = new PaletteRequest { Width = -10, Height = 0, Depth = 1000 };
        
        var createWarehouse = await DataHelper.GenerateWarehouse(warehouseId);
        
        // Act
        var createPalette = await _sut
            .PostAsync(warehouseId, paletteId, request, CancellationToken.None);

        // Assert
        createWarehouse.StatusCode.Should().Be(HttpStatusCode.Created);
        createPalette.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var error = await createPalette.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error!.Status.Should().Be(400);
        error.Errors.Should().ContainKey("Depth", "Palette depth too big");
        error.Errors.Should().ContainKey("Width", "Palette width should not be zero or negative.");
        error.Errors.Should().ContainKey("Height", "Palette height should not be zero or negative.");
        error.Errors.Should().ContainKey("Height", "'Height' must not be empty.");
    }
}
