using System.Net;
using FluentAssertions;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Box;

public sealed class DeleteBoxControllerTests : TestControllerBase
{
    public DeleteBoxControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
    }

    [Fact(DisplayName = "DeleteExistingBox")]
    public async Task Delete_ReturnsOK_WhenBoxExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();

        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        await GenerateBox(paletteId, boxId);
        
        // Act
        var deleteResponse = await Sut.BoxClient.DeleteAsync(boxId, CancellationToken.None);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact(DisplayName = "DeleteNonExistingBox")]
    public async Task Delete_ReturnsNotFound_WhenBoxDoesNotExist()
    {
        // Act
        var deleteResponse = await Sut.BoxClient.DeleteAsync(Guid.NewGuid(), CancellationToken.None);
    
        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
