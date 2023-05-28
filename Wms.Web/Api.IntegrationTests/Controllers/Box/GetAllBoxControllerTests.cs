using FluentAssertions;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Controllers.Box;

public sealed class GetAllBoxControllerTests : TestControllerBase
{
    private readonly IWmsClient _sut;

    public GetAllBoxControllerTests(TestApplication apiFactory)
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));
    }

    [Fact(DisplayName = "GetAllBoxes")]
    public async Task GetAll_ShouldReturnPalettes()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxIdFirst = Guid.NewGuid();
        var boxIdSecond = Guid.NewGuid();
        
        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        var boxOne = await GenerateBox(paletteId, boxIdFirst);
        var boxTwo = await GenerateBox(paletteId, boxIdSecond);

        // Act
        var boxes =
            await _sut.BoxClient.GetAllAsync(paletteId, 0, 2, CancellationToken.None);

        var box =
            await _sut.BoxClient.GetAllAsync(paletteId, 1, 1, CancellationToken.None);

        // Assert
        boxes?.Count.Should().Be(2);
        box?.Count.Should().Be(1);
        boxes?.FirstOrDefault().Should().BeEquivalentTo(boxOne, 
            options => options
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt));
        boxes?.LastOrDefault().Should().BeEquivalentTo(boxTwo, 
            options => options
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt));
        box?.Single().Should().BeEquivalentTo(boxTwo, 
            options => options
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt));
    }

    [Fact(DisplayName = "GetDeletedBoxes")]
    public async Task GetAllDeleted_ShouldReturnDeletedBoxes()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxIdFirst = Guid.NewGuid();
        var boxIdSecond = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        var boxOne = await GenerateBox(paletteId, boxIdFirst);
        var boxTwo = await GenerateBox(paletteId, boxIdSecond);

        await DeleteBox(boxIdFirst);
        await DeleteBox(boxIdSecond);

        // Act
        var responseAll =
            await _sut.BoxClient.GetAllDeletedAsync(paletteId, 0, 2, CancellationToken.None);

        var responseOne =
            await _sut.BoxClient.GetAllDeletedAsync(paletteId, 1, 1, CancellationToken.None);

        // Assert
        responseAll?.Count.Should().Be(2);
        responseOne?.Count.Should().Be(1);
        responseAll?.FirstOrDefault().Should().BeEquivalentTo(boxOne, 
            options => options
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt));
        responseAll?.LastOrDefault().Should().BeEquivalentTo(boxTwo, 
            options => options
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt));
        responseOne?.Single().Should().BeEquivalentTo(boxTwo, 
            options => options
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt));
    }
}
