using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Wms.Web.Api.IntegrationTests.Extensions;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms.PaletteControllerTests;

public sealed class GetAllPaletteControllerTests : TestControllerBase
{
    private readonly IPaletteClient _sut;

    public GetAllPaletteControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new PaletteClient(HttpClient);
    }
    /*
    [Fact(DisplayName = "GetAllPalettes")]
    public async Task GetAll_ShouldReturnPalettes()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId1 = Guid.NewGuid();
        var paletteId2 = Guid.NewGuid();
        var paletteRequest = new PaletteRequest{ Width = 10, Height = 10, Depth = 10 };
        
        await DataHelper.GenerateWarehouse(warehouseId);

        var createPalette1 = await DataHelper
            .GeneratePalette(warehouseId, paletteId1, paletteRequest);
        var createPalette2 = await DataHelper
            .GeneratePalette(warehouseId, paletteId2, paletteRequest);
        var createdPalette1 = await createPalette1.Content.ReadFromJsonAsync<PaletteRequest>();
        var createdPalette2 = await createPalette2.Content.ReadFromJsonAsync<PaletteRequest>();
        
        // Act
        var responseAll = 
            await _sut.GetAllAsync(warehouseId, 0, 2, CancellationToken.None);
        
        var responseOne =
            await _sut.GetAllAsync(warehouseId, 1, 1, CancellationToken.None);
        
        // Assert
        createPalette1.StatusCode.Should().Be(HttpStatusCode.Created);
        createPalette2.StatusCode.Should().Be(HttpStatusCode.Created);
        responseAll?.Count.Should().Be(2);
        responseOne?.Count.Should().Be(1);
        responseAll!.FirstOrDefault().Should().BeEquivalentTo(createdPalette1);
        responseAll!.LastOrDefault().Should().BeEquivalentTo(createdPalette2);
        responseOne!.Single().Should().BeEquivalentTo(createdPalette2);
    }

    [Fact(DisplayName = "GetDeletedPalettes")]
    public async Task GetAllDeleted_ShouldReturnDeletedPalettes()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId1 = Guid.NewGuid();
        var paletteId2 = Guid.NewGuid();
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        
        await DataHelper.GenerateWarehouse(warehouseId);

        var createPalette1 = await DataHelper
            .GeneratePalette(warehouseId, paletteId1, paletteRequest);
        var createPalette2 = await DataHelper
            .GeneratePalette(warehouseId, paletteId2, paletteRequest);
        var createdPalette1 = await createPalette1.Content.ReadFromJsonAsync<PaletteRequest>();
        var createdPalette2 = await createPalette2.Content.ReadFromJsonAsync<PaletteRequest>();

        await DataHelper.DeletePalette(paletteId1);
        await DataHelper.DeletePalette(paletteId2);

        // Act
        var responseAll = 
            await _sut.GetAllDeletedAsync(warehouseId, 0, 2, CancellationToken.None);
        
        var responseOne =
            await _sut.GetAllDeletedAsync(warehouseId, 1, 1, CancellationToken.None);

        // Assert
        responseAll?.Count.Should().Be(2);
        responseOne?.Count.Should().Be(1);
        responseAll!.FirstOrDefault().Should().BeEquivalentTo(createdPalette1);
        responseAll!.LastOrDefault().Should().BeEquivalentTo(createdPalette2);
        responseOne!.Single().Should().BeEquivalentTo(createdPalette2);
    }*/
}
