using FluentAssertions;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Box;

public sealed class GetAllBoxControllerTests : TestControllerBase
{
    public GetAllBoxControllerTests(TestApplication apiFactory)
        : base(apiFactory)
    {
    }

    [Theory(DisplayName = "GetAllBoxes")]
    [InlineData(0, 2, 2)]
    [InlineData(1, 1, 1)]
    public async Task GetAll_ShouldReturnPalettes(int offset, int size, int expectedCount)
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxIdFirst = Guid.NewGuid();
        var boxIdSecond = Guid.NewGuid();
        
        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        await GenerateBox(paletteId, boxIdFirst);
        
        var boxTwo = await GenerateBox(paletteId, boxIdSecond);

        // Act
        var boxes =
            await Sut.BoxClient.GetAllAsync(paletteId, offset, size, CancellationToken.None);

        // Assert
        boxes?.Count.Should().Be(expectedCount);
        boxes.Should().ContainEquivalentOf(boxTwo);
    }

    [Theory(DisplayName = "GetDeletedBoxes")]
    [InlineData(0, 2, 2)]
    [InlineData(1, 1, 1)]
    public async Task GetAllDeleted_ShouldReturnDeletedBoxes(int offset, int size, int expectedCount)
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxIdFirst = Guid.NewGuid();
        var boxIdSecond = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        await GenerateBox(paletteId, boxIdFirst);
        
        var boxTwo = await GenerateBox(paletteId, boxIdSecond);

        // Act
        var existingDeletedBoxes = await Sut.BoxClient
            .GetAllDeletedAsync(paletteId, 0, 10, CancellationToken.None);

        await DeleteBox(boxIdFirst);
        await DeleteBox(boxIdSecond);
        
        var boxes =
            await Sut.BoxClient.GetAllDeletedAsync(
                paletteId, 
                existingDeletedBoxes?.Count + offset, 
                size, 
                CancellationToken.None);

        // Assert
        boxes?.Count.Should().Be(expectedCount);
        boxes.Should().ContainEquivalentOf(boxTwo);
    }
}
