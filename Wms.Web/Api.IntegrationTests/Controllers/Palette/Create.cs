using FluentAssertions;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Wms.Web.Common.Exceptions;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Controllers.Palette;

public sealed class Create : TestControllerBase
{
    private readonly IWmsClient _sut;
    
    public Create(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));
    }
    
    [Theory(DisplayName = "CreatePalette")]
    [InlineData(10, 10, 10, 1000)]
    [InlineData(5.5, 5.5, 5.5, 166.375)]
    [InlineData(7.25, 8.47, 9.34, 573.54605)]
    public async Task Create_ShouldCreatePalette_WithCalculatedVolume(
        decimal width,
        decimal height,
        decimal depth,
        decimal expectedVolume)
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var request = new PaletteRequest { Width = width, Height = height, Depth = depth };
        
        await GenerateWarehouse(warehouseId);
        
        var createPalette = await _sut.PaletteClient
            .CreateAsync(warehouseId, paletteId, request, CancellationToken.None);
        
        // Assert
        createPalette.Should().BeEquivalentTo(request);
        createPalette?.Volume.Should().Be(expectedVolume);
        createPalette?.Weight.Should().Be(30);
    }
    
    [Fact(DisplayName = "CreatePaletteConflict")]
    public async Task Create_ShouldReturnConflict_IfPaletteExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var request = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        
        await GenerateWarehouse(warehouseId);
        
        // Act
        await _sut.PaletteClient
            .CreateAsync(warehouseId, paletteId, request, CancellationToken.None);
        async Task Act() => await _sut.PaletteClient.CreateAsync(warehouseId, paletteId, request, CancellationToken.None);
        
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
        
        await GenerateWarehouse(warehouseId);
        
        // Act
        async Task Act() => await _sut.PaletteClient.CreateAsync(warehouseId, paletteId, request, CancellationToken.None);
        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);

        // Assert
        exception.ErrorCode.Should().Be("incorrect_http_request");
        exception.Message.Should().Be("API request failed!");
        exception.ProblemDetails?.Status.Should().Be(400);
        exception.ProblemDetails?.Errors?.Count.Should().Be(3);
        exception.ProblemDetails!.Errors!["Depth"].Should().Contain("Palette depth too big");
        exception.ProblemDetails!.Errors!["Width"].Should().Contain("Palette width should not be zero or negative.");
        exception.ProblemDetails!.Errors!["Height"].Should().Contain("'Height' must not be empty.");
        exception.ProblemDetails!.Errors!["Height"].Should().Contain("Palette height should not be zero or negative.");
    }
}
