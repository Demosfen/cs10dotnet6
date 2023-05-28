using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms.BoxControllerTests;

public sealed class GetAllBoxControllerTests : TestControllerBase
{
    private readonly IBoxClient _sut;

    public GetAllBoxControllerTests(TestApplication apiFactory)
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri(BaseUri)
        });

        _sut = new BoxClient(HttpClient);
    }
/*

    [Fact(DisplayName = "GetAllBoxes")]
    public async Task GetAll_ShouldReturnPalettes()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxIdFirst = Guid.NewGuid();
        var boxIdSecond = Guid.NewGuid();
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        var boxRequestFirst = new BoxRequest
        {
            Width = 1, Depth = 1, Height = 1,
            Weight = 1,
            ExpiryDate = new DateTime(2007, 1, 1),
            ProductionDate = new DateTime(2006, 1, 1)
        };
        var boxRequestSecond = new BoxRequest
        {
            Width = 2, Depth = 2, Height = 2,
            Weight = 2,
            ExpiryDate = new DateTime(2007, 1, 1),
            ProductionDate = new DateTime(2006, 1, 1)
        };

        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
        await DataHelper.GenerateBox(paletteId, boxIdFirst, boxRequestFirst);
        await DataHelper.GenerateBox(paletteId, boxIdSecond, boxRequestSecond);

        // Act
        var responseAll =
            await _sut.GetAllAsync(paletteId, 0, 2, CancellationToken.None);

        var responseOne =
            await _sut.GetAllAsync(paletteId, 1, 1, CancellationToken.None);

        // Assert
        responseAll?.Count.Should().Be(2);
        responseOne?.Count.Should().Be(1);
        responseAll!.FirstOrDefault().Should().BeEquivalentTo(boxRequestFirst);
        responseAll!.LastOrDefault().Should().BeEquivalentTo(boxRequestSecond);
        responseOne!.Single().Should().BeEquivalentTo(boxRequestSecond);
    }

    [Fact(DisplayName = "GetDeletedBoxes")]
    public async Task GetAllDeleted_ShouldReturnDeletedBoxes()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxIdFirst = Guid.NewGuid();
        var boxIdSecond = Guid.NewGuid();
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        var boxRequestFirst = new BoxRequest
        {
            Width = 1, Depth = 1, Height = 1,
            Weight = 1,
            ExpiryDate = new DateTime(2007, 1, 1),
            ProductionDate = new DateTime(2006, 1, 1)
        };
        var boxRequestSecond = new BoxRequest
        {
            Width = 2, Depth = 2, Height = 2,
            Weight = 2,
            ExpiryDate = new DateTime(2007, 1, 1),
            ProductionDate = new DateTime(2006, 1, 1)
        };

        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
        await DataHelper.GenerateBox(paletteId, boxIdFirst, boxRequestFirst);
        await DataHelper.GenerateBox(paletteId, boxIdSecond, boxRequestSecond);

        await DataHelper.DeleteBox(boxIdFirst);
        await DataHelper.DeleteBox(boxIdSecond);

        // Act
        var responseAll =
            await _sut.GetAllDeletedAsync(paletteId, 0, 2, CancellationToken.None);

        var responseOne =
            await _sut.GetAllDeletedAsync(paletteId, 1, 1, CancellationToken.None);

        // Assert
        responseAll?.Count.Should().Be(2);
        responseOne?.Count.Should().Be(1);
        responseAll!.FirstOrDefault().Should().BeEquivalentTo(boxRequestFirst);
        responseAll!.LastOrDefault().Should().BeEquivalentTo(boxRequestSecond);
        responseOne!.Single().Should().BeEquivalentTo(boxRequestSecond);
    }*/
}
