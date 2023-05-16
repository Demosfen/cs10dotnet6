using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Api.IntegrationTests.Abstract;
using Wms.Web.Api.IntegrationTests.Extensions;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms.PaletteControllerTest;

public sealed class GetByIdWPaletteControllerTests : TestControllerBase
{
    private readonly IPaletteClient _sut;
    private readonly WmsDataHelper _dataHelper;
    private const string BaseUri = "http://localhost";

    public GetByIdWPaletteControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri(BaseUri)
        });
        
        _sut = new PaletteClient(HttpClient, options);

        _dataHelper = new WmsDataHelper(apiFactory);
    }
    
    [Fact(DisplayName = "GetPaletteById")]
    public async Task GetById_ReturnPalette_WhenPaletteExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        
        await _dataHelper.GenerateWarehouse(warehouseId);
        
        var createPalette = await _dataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
        var createdPalette = await createPalette.Content.ReadFromJsonAsync<PaletteResponse>();

        // Act
        var response = await _sut.GetByIdAsync(paletteId, 0, 0, CancellationToken.None);
        
        // Assert
        response.Should().BeEquivalentTo(createdPalette);
    }

    [Fact(DisplayName = "NotFoundWhenPaletteDoesNotExist")]
    public async Task GetById_ReturnsNotFound_WhenPaletteDoesNotExist()
    {
        // Act
        try
        {
            await _sut.GetByIdAsync(Guid.NewGuid(), 0, 0, CancellationToken.None);
        }
        catch (HttpRequestException response)
        {
            response.StatusCode.HasValue.Should().Be(true);
            response.StatusCode?.Should().Be(HttpStatusCode.NotFound);
        }
    }
    
    [Fact(DisplayName = "GetPaletteByIdIfDeleted")]
    public async Task GetById_ReturnPalette_WhenPaletteWasDeleted()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var paletteRequest = new PaletteRequest { Width = 10, Depth = 10, Height = 10 };
    
        await _dataHelper.GenerateWarehouse(warehouseId);

        var createPalette = await _dataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
        var createdPalette = await createPalette.Content.ReadFromJsonAsync<PaletteRequest>();
    
        var deletePalette = await _dataHelper.DeletePalette(paletteId);
        
        // Act
        var response = await _sut.GetByIdAsync(paletteId, 0, 0, CancellationToken.None);
        
        // Assert
        deletePalette.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().BeEquivalentTo(createdPalette);
    }
    
    [Fact(DisplayName = "GetNonEmptyPaletteById")]
    public async Task GetById_ReturnPalette_WhenPaletteNonEmpty()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        var boxRequest = new BoxRequest
        {
            Width = 1, Depth = 1, Height = 1,
            Weight = 1, ExpiryDate = new DateTime(2007, 1, 1)
        };
        
        var createWarehouse = await _dataHelper.GenerateWarehouse(warehouseId);
        var createPalette = await _dataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
        var createBox = await _dataHelper.GenerateBox(paletteId, boxId, boxRequest);

        // Act
        var response = await _sut.GetByIdAsync(paletteId, 0, 1, CancellationToken.None);
        
        // Assert
        createWarehouse.StatusCode.Should().Be(HttpStatusCode.Created);
        createPalette.StatusCode.Should().Be(HttpStatusCode.Created);
        createBox.StatusCode.Should().Be(HttpStatusCode.Created);
        response?.Weight.Should().Be(31);
        response?.Volume.Should().Be(1001);
    }
}
