using FluentAssertions;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Controllers.Box;

public sealed class GetAll : TestControllerBase
{
    private readonly IWmsClient _sut;

    public GetAll(TestApplication apiFactory)
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));
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
            await _sut.BoxClient.GetAllAsync(paletteId, offset, size, CancellationToken.None);

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

        await DeleteBox(boxIdFirst);
        await DeleteBox(boxIdSecond);

        // Act
        var boxes =
            await _sut.BoxClient.GetAllDeletedAsync(paletteId, offset, size, CancellationToken.None);

        // Assert
        boxes?.Count.Should().Be(expectedCount);
        boxes.Should().ContainEquivalentOf(boxTwo);
    }
}
