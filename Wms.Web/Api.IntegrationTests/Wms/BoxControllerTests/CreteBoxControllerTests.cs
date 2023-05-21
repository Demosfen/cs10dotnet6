using FluentAssertions;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Extensions;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Wms.Web.Common.Exceptions;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms.BoxControllerTests;

public sealed class CreteBoxControllerTests : TestControllerBase
{
    private readonly IBoxClient _sut;
    
    public CreteBoxControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new BoxClient(HttpClient);
    }
    
    [Fact(DisplayName = "CreateBox")]
    public async Task Create_ShouldCreateBox()
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
    
        // Act
        var createBox = await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);

        // Assert
        createBox.Should().BeEquivalentTo(boxRequest);
    }
    
    [Fact(DisplayName = "CreateBoxConflict")]
    public async Task Create_ShouldThrowConflict_IfBoxAlreadyExist()
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
        
        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
    
        // Act
        await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        async Task Act() => await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);

        var exception = await Assert.ThrowsAsync<EntityAlreadyExistException>(Act);
    
        // Assert
        exception.ErrorCode.Should().Be("entity_already_exist");
        exception.Message.Should().Be($"The entity with id={boxId} already exist");
        exception.ShortDescription.Should().Be("The entity with specified id already exist");
    }
    
    [Fact(DisplayName = "BoxSizeValidation")]
    public async Task Create_ShouldReturnError_IfBoxSizeIncorrect()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        var boxRequest = new BoxRequest
        {
            Width = -1, Depth = 0, Height = 1000,
            Weight = 1, ExpiryDate = new DateTime(2007, 1, 1)
        };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
    
        // Act
        async Task Act() => await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);

        // Assert
        exception.ErrorCode.Should().Be("incorrect_http_request");
        exception.Message.Should().Be("API request failed!");
        exception.ProblemDetails?.Status.Should().Be(400);
        exception.ProblemDetails?.Errors?.Count.Should().Be(3);
        exception.ProblemDetails!.Errors!.ContainsKey("Depth").Should().BeTrue();
        exception.ProblemDetails!.Errors!["Depth"].Should().Contain("'Depth' must not be empty.");
        exception.ProblemDetails!.Errors!.ContainsKey("Depth").Should().BeTrue();
        exception.ProblemDetails!.Errors!["Depth"].Should().Contain("Box depth should not be zero or negative.");
        exception.ProblemDetails!.Errors!.ContainsKey("Width").Should().BeTrue();
        exception.ProblemDetails!.Errors!["Width"].Should().Contain("Box width should not be zero or negative.");
        exception.ProblemDetails!.Errors!.ContainsKey("Height").Should().BeTrue();
        exception.ProblemDetails!.Errors!["Height"].Should().Contain("Box height too big");
    }
    
    [Fact(DisplayName = "BoxExpiryAndProductionDatesValidation")]
    public async Task Create_ShouldReturnError_IfBoxExpiryLowerThanProduction()
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
            ProductionDate = new DateTime(2008,1,1),
            ExpiryDate = new DateTime(2007, 1, 1)
        };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);

        // Act
        async Task Act() => await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        var exception = await Assert.ThrowsAsync<UnprocessableEntityException>(Act);
    
        // Assert
        exception.ErrorCode.Should().Be("unprocessable_entity");
        exception.ShortDescription.Should().Be("Unprocessable entity properties was requested");
        exception.ProblemDetails?.Status.Should().Be(422);
        exception.ProblemDetails?.Type.Should().Be("entity_expiry_incorrect");
        exception.ProblemDetails?.Title.Should().Be(
            "The entity with specified Expiry date cannot be created");
        exception.ProblemDetails?.Detail.Should().Be(
                $"The entity with id={boxId} has incorrect Expiry date. Probably Expiry lower than Production date."); 
    }
    
    [Fact(DisplayName = "EmptyExpiryAndProductionDatesValidation")]
    public async Task Create_ShouldReturnError_IfBoxExpiryAndProductionEmpty()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        var boxRequest = new BoxRequest
        {
            Width = 1, Depth = 1, Height = 1,
            Weight = 1
        };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);
    
        // Act
        async Task Act() => await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);
    
        // Assert
        exception.ErrorCode.Should().Be("incorrect_http_request");
        exception.Message.Should().Be("API request failed");
        exception.ProblemDetails?.Detail.Should().Be("Both Production and Expiry date should not be null");
        exception.ProblemDetails?.Status.Should().Be(500); //TODO change to BadRequest?
    }
    
    [Fact(DisplayName = "BoxOversizeException")]
    public async Task Create_ShouldReturnError_IfBoxBiggerThanPalette()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        var paletteRequest = new PaletteRequest { Width = 10, Height = 10, Depth = 10 };
        var boxRequest = new BoxRequest
        {
            Width = 15, Depth = 15, Height = 15,
            Weight = 1,
            ProductionDate = new DateTime(2007,1,1),
            ExpiryDate = new DateTime(2008,1,1)
        };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);

        // Act
        async Task Act() => await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        var exception = await Assert.ThrowsAsync<UnprocessableEntityException>(Act);
    
        // Assert
        exception.ErrorCode.Should().Be("unprocessable_entity");
        exception.ProblemDetails?.Type.Should().Be("unit_oversize");
        exception.ProblemDetails?.Status.Should().Be(422);
        exception.ProblemDetails?.Title.Should().Be("The box does not match the dimensions of the pallet");
        exception.ProblemDetails?.Instance.Should().Be(
            $"http://localhost/api/v1/palettes/{paletteId}?boxId={boxId}");
    }
}
