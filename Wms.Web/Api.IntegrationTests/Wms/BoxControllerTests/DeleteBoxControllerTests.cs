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

public sealed class DeleteBoxControllerTests : TestControllerBase
{
    private readonly IBoxClient _sut;

    public DeleteBoxControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri(BaseUri)
        });
        
        _sut = new BoxClient(HttpClient, options);
    }
    
    [Fact(DisplayName = "DeleteExistingBox")]
    public async Task Delete_ReturnsOK_WhenBoxExist()
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
        await DataHelper.GenerateBox(paletteId, boxId, boxRequest);
        
        // Act
        var deleteResponse = await _sut.DeleteAsync(boxId, CancellationToken.None);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(DisplayName = "DeleteNonExistingBox")]
    public async Task Delete_ReturnsNotFound_WhenBoxDoesNotExist()
    {
        // Act
        var deleteResponse = await _sut.DeleteAsync(Guid.NewGuid(), CancellationToken.None);
    
        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
