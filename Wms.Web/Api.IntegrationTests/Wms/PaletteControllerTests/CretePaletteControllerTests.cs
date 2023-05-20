using FluentAssertions;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Wms.Web.Common.Exceptions;
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
        
        await DataHelper.GenerateWarehouse(warehouseId);
        
        var createPalette = await _sut
            .CreateAsync(warehouseId, paletteId, request, CancellationToken.None);
        
        // Assert
        createPalette.Should().BeEquivalentTo(request);
        createPalette?.Volume.Should().Be(request.Width * request.Height * request.Depth);
        createPalette?.Weight.Should().Be(30);
    }
    
    [Fact(DisplayName = "CreatePaletteConflict")]
    public async Task Create_ShouldReturnConflict_IfPaletteExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var request = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        
        // Act
        await _sut
            .CreateAsync(warehouseId, paletteId, request, CancellationToken.None);
        async Task Act() => await _sut.CreateAsync(warehouseId, paletteId, request, CancellationToken.None);
        
        var exception = await Assert.ThrowsAsync<EntityAlreadyExistException>(Act);
        exception.Id.Should().Be(paletteId);
        exception.Message.Should().Be($"The entity with id={paletteId} already exist");
    }
    
    [Fact(DisplayName = "PaletteSizeValidation")]
    public async Task Create_ShouldReturnError_IfPaletteSizeIncorrect()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var request = new PaletteRequest { Width = -10, Height = 0, Depth = 1000 };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        
        // Act
        async Task Act() => await _sut.CreateAsync(warehouseId, paletteId, request, CancellationToken.None);
        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);

        // Assert
        exception.ErrorCode.Should().Be("incorrect_http_request");
        exception.Message.Should().Be("API request failed!");
        exception.ProblemDetails?.Status.Should().Be(400);
        exception.ProblemDetails?.Errors?.Count.Should().Be(3);
        exception.ProblemDetails!.Errors!.ContainsKey("Depth").Should().BeTrue();
        exception.ProblemDetails!.Errors!["Depth"].Should().Contain("Palette depth too big");
        exception.ProblemDetails!.Errors!.ContainsKey("Width").Should().BeTrue();
        exception.ProblemDetails!.Errors!["Width"].Should().Contain("Palette width should not be zero or negative.");
        exception.ProblemDetails!.Errors!.ContainsKey("Height").Should().BeTrue();
        exception.ProblemDetails!.Errors!["Height"].Should().Contain("'Height' must not be empty.");
        exception.ProblemDetails!.Errors!.ContainsKey("Height").Should().BeTrue();
        exception.ProblemDetails!.Errors!["Height"].Should().Contain("Palette height should not be zero or negative.");
    }
}
