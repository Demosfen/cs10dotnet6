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

namespace Wms.Web.Api.IntegrationTests.Wms.BoxControllerTests;

public sealed class GetByIdBoxControllerTests : TestControllerBase
{
    private readonly IBoxClient _sut;

    public GetByIdBoxControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri(BaseUri)
        });
        
        _sut = new BoxClient(HttpClient, options);
    }
    
    [Fact(DisplayName = "GetBoxById")]
    public async Task GetById_ReturnBox_WhenBoxExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        var boxRequest = new BoxRequest
        {
            Width = 1, Depth = 1, Height = 1,
            Weight = 1, 
            ExpiryDate = new DateTime(2007, 1, 1),
            ProductionDate = new DateTime(2006,1,1)
        };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
        var createBox = await DataHelper.GenerateBox(paletteId, boxId, boxRequest);
        var createdBox = await createBox.Content.ReadFromJsonAsync<BoxResponse>();

        // Act
        var response = await _sut.GetByIdAsync(boxId, CancellationToken.None);
        
        // Assert
        response.Should().BeEquivalentTo(createdBox);
    }
    
    [Fact(DisplayName = "NotFoundWhenBoxDoesNotExist")]
    public async Task GetById_ReturnsNotFound_WhenBoxDoesNotExist()
    {
        // Act
        try
        {
            await _sut.GetByIdAsync(Guid.NewGuid());
        }
        catch (HttpRequestException response)
        {
            response.StatusCode.HasValue.Should().Be(true);
            response.StatusCode?.Should().Be(HttpStatusCode.NotFound);
        }
    }

    [Fact(DisplayName = "GetBoxByIdIfDeleted")]
    public async Task GetById_ReturnBox_WhenBoxWasDeleted()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        var boxRequest = new BoxRequest
        {
            Width = 1, Depth = 1, Height = 1,
            Weight = 1, 
            ExpiryDate = new DateTime(2007, 1, 1),
            ProductionDate = new DateTime(2006,1,1)
        };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
        var createBox = await DataHelper.GenerateBox(paletteId, boxId, boxRequest);
        var createdBox = await createBox.Content.ReadFromJsonAsync<BoxResponse>();

        await DataHelper.DeleteBox(boxId);
        
        // Act
        var response = await _sut.GetByIdAsync(boxId);

        // Assert
        response.Should().BeEquivalentTo(createdBox);
    }
}
