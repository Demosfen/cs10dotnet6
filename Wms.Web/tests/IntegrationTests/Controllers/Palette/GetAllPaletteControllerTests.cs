using FluentAssertions;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Palette;

public sealed class GetAllPaletteControllerTests : TestControllerBase
{
    public GetAllPaletteControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
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
        var palettes = await Sut.PaletteClient
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
        var palettes = await Sut.PaletteClient
            .GetAllDeletedAsync(warehouseId, offset, size, CancellationToken.None);
        
        // Assert
        palettes?.Count.Should().Be(expectedCount);
        palettes.Should().ContainEquivalentOf(createdPalette2);
    }
}
