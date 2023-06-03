using FluentAssertions;
using Wms.Web.Client.Custom.Abstract;
using Wms.Web.Client.Custom.Concrete;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Palette;

public sealed class GetAllPaletteControllerTests : TestControllerBase
{
    private readonly IWmsClient _sut;

    public GetAllPaletteControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));
    }

    [Theory(DisplayName = "GetAllPalettes")]
    [InlineData(0, 2, 2)]
    [InlineData(1, 1, 1)]
    public async Task GetAll_ShouldReturnPalettes(int offset, int size, int expectedCount)
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId1 = Guid.NewGuid();
        var paletteId2 = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId1);
        
        var createdPalette2 = await GeneratePalette(warehouseId, paletteId2);

        // Act
        var palettes = await _sut.PaletteClient
            .GetAllAsync(warehouseId, offset, size, CancellationToken.None);
        
        // Assert
        palettes?.Count.Should().Be(expectedCount);
        palettes.Should().ContainEquivalentOf(createdPalette2);
    }

    [Theory(DisplayName = "GetAllDeletedPalettes")]
    [InlineData(0, 2, 2)]
    [InlineData(1, 1, 1)]
    public async Task GetAllDeleted_ShouldReturnDeletedPalettes(int offset, int size, int expectedCount)
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId1 = Guid.NewGuid();
        var paletteId2 = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId1);
        
        var createdPalette2 = await GeneratePalette(warehouseId, paletteId2);

        await DeletePalette(paletteId1);
        await DeletePalette(paletteId2);

        // Act
        var palettes = await _sut.PaletteClient
            .GetAllDeletedAsync(warehouseId, offset, size, CancellationToken.None);
        
        // Assert
        palettes?.Count.Should().Be(expectedCount);
        palettes.Should().ContainEquivalentOf(createdPalette2);
    }
}
