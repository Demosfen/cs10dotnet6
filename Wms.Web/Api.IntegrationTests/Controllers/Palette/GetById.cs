using System.Net;
using FluentAssertions;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Api.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Controllers.Palette;

public sealed class GetByIdWPaletteControllerTests : TestControllerBase
{
    private readonly IWmsClient _sut;

    public GetByIdWPaletteControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));    }

    [Fact(DisplayName = "GetPaletteById")]
    public async Task GetById_ReturnPalette_WhenPaletteExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        
        await GenerateWarehouse(warehouseId);
        
        var createdPalette = await GeneratePalette(warehouseId, paletteId);

        // Act
        var response = await _sut.PaletteClient
            .GetByIdAsync(paletteId, 0, 0, CancellationToken.None);
        
        // Assert
        response.Should().BeEquivalentTo(createdPalette);
    }

    [Fact(DisplayName = "NotFoundWhenPaletteDoesNotExist")]
    public async Task GetById_ReturnsNotFound_WhenPaletteDoesNotExist()
    {
        // Act
        async Task Act() => await _sut.PaletteClient
            .GetByIdAsync(Guid.NewGuid(), 0,0, CancellationToken.None);
        
        var exception = await Assert.ThrowsAsync<HttpRequestException>(Act);
        
        // Assert
        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact(DisplayName = "GetPaletteByIdIfDeleted")]
    public async Task GetById_ReturnPalette_WhenPaletteWasDeleted()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);

        var createdPalette = await GeneratePalette(warehouseId, paletteId);
        
        await DeletePalette(paletteId);
        
        // Act
        var response = await _sut.PaletteClient
            .GetByIdAsync(paletteId, 0, 0, CancellationToken.None);
        
        // Assert
        response.Should().BeEquivalentTo(createdPalette);
    }
    
    [Fact(DisplayName = "GetNonEmptyPaletteById")]
    public async Task GetById_ReturnPalette_WhenPaletteNonEmpty()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        
        await GenerateWarehouse(warehouseId);
        
        var createdPalette = await GeneratePalette(warehouseId, paletteId);

        var createBox = await _sut.BoxClient.CreateAsync(
            paletteId,
            boxId,
            new BoxRequest
            {
                Width = 1,
                Depth = 1,
                Height = 1,
                Weight = 1,
                ProductionDate = new DateTime(2007, 1, 1)
            });

        // Act
        var response = await _sut.PaletteClient
            .GetByIdAsync(paletteId, 0, 1, CancellationToken.None);
        
        // Assert
        response?.Weight.Should().Be(31);
        response?.Volume.Should().Be(1001);
        response?.Boxes.Count.Should().Be(1);
        response?.Boxes.SingleOrDefault().Should().BeEquivalentTo(createBox);
    }
}
