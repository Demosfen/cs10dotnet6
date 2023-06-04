using FluentAssertions;
using Wms.Web.Common.Exceptions;
using Wms.Web.Contracts.Requests;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Box;

public sealed class CreteBoxControllerTests : TestControllerBase
{
    public CreteBoxControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
    }
    
    [Fact(DisplayName = "CreateBox")]
    public async Task Create_ShouldCreateBox()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        
        var boxRequest = new BoxRequest
        {
            Width = 1, Depth = 1, Height = 1,
            Weight = 1, 
            ExpiryDate = new DateTime(2007, 1, 1, 0,0,0,0, DateTimeKind.Utc),
            ProductionDate = new DateTime(2006,1,1, 0,0,0,0, DateTimeKind.Utc)
        };
        
        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
    
        // Act
        var createBox = await Sut.BoxClient.CreateAsync(paletteId, boxId, boxRequest);

        // Assert
        createBox.Should().BeEquivalentTo(boxRequest);
    }

    [Theory(DisplayName = "BoxVolumeCalculation")]
    [InlineData(1, 1, 1, 1)]
    [InlineData(5.5, 5.5, 5.5, 166.375)]
    [InlineData(9.15, 8.29, 7.63, 578.762205)]
    public async Task Create_ShouldCreateBox_WithCalculatedVolume(
        decimal width, 
        decimal height, 
        decimal depth, 
        decimal expectedVolume)
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        var boxRequest = new BoxRequest
        {
            Width = width,
            Height = height,
            Depth = depth, 
            Weight = 1, 
            ExpiryDate = new DateTime(2007, 1, 1, 0,0,0,0, DateTimeKind.Utc)
        };
        
        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        
        // Act
        var createdBox = await Sut.BoxClient.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        
        // Assert
        createdBox?.Volume.Should().Be(expectedVolume);
    }
    
    [Fact(DisplayName = "CreateBoxConflict")]
    public async Task Create_ShouldThrowConflict_IfBoxAlreadyExist()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        var boxRequest = new BoxRequest
        {
            Width = 1, Depth = 1, Height = 1,
            Weight = 1, ExpiryDate = new DateTime(2007, 1, 1, 0,0,0,0, DateTimeKind.Utc)
        };
        
        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
        await GenerateBox(paletteId, boxId);
    
        // Act
        async Task Act() => await Sut.BoxClient.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);

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
        
        var boxRequest = new BoxRequest
        {
            Width = -1, Depth = 0, Height = 1000,
            Weight = 1, ExpiryDate = 
                new DateTime(2007, 1, 1, 0,0,0,0, DateTimeKind.Utc)
        };
        
        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
    
        // Act
        async Task Act() => await Sut.BoxClient.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);

        // Assert
        exception.ErrorCode.Should().Be("incorrect_http_request");
        exception.Message.Should().Be("API request failed!");
        exception.ProblemDetails?.Status.Should().Be(400);
        exception.ProblemDetails?.Errors?.Count.Should().Be(3);
        exception.ProblemDetails!.Errors!["Depth"].Should().Contain("'Depth' must not be empty.");
        exception.ProblemDetails!.Errors!["Depth"].Should().Contain("Box depth should not be zero or negative.");
        exception.ProblemDetails!.Errors!["Width"].Should().Contain("Box width should not be zero or negative.");
        exception.ProblemDetails!.Errors!["Height"].Should().Contain("Box height too big");
    }
    
    [Fact(DisplayName = "BoxExpiryAndProductionDatesValidation")]
    public async Task Create_ShouldReturnError_IfBoxExpiryLowerThanProduction()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();

        var boxRequest = new BoxRequest
        {
            Width = 1, Depth = 1, Height = 1,
            Weight = 1, 
            ProductionDate = new DateTime(2008,1,1, 0,0,0,0, DateTimeKind.Utc),
            ExpiryDate = new DateTime(2007, 1, 1, 0,0,0,0, DateTimeKind.Utc)
        };
        
        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);

        // Act
        async Task Act() => await Sut.BoxClient.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);
    
        // Assert
        exception.ErrorCode.Should().Be("incorrect_http_request");
        exception.Message.Should().Be("API request failed!");
        exception.ProblemDetails?.Status.Should().Be(400);
        exception.ProblemDetails?.Type.Should().Be("entity_expiry_incorrect");
        exception.ProblemDetails?.Title
            .Should().Be("The entity with specified Expiry and Production dates cannot be created"); 
    }
    
    [Fact(DisplayName = "EmptyExpiryAndProductionDatesValidation")]
    public async Task Create_ShouldReturnError_IfBoxExpiryAndProductionEmpty()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        var boxRequest = new BoxRequest
        {
            Width = 1, Depth = 1, Height = 1,
            Weight = 1
        };
        
        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);
    
        // Act
        async Task Act() => await Sut.BoxClient.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);
    
        // Assert
        exception.ErrorCode.Should().Be("incorrect_http_request");
        exception.Message.Should().Be("API request failed!");
        exception.ProblemDetails?.Status.Should().Be(400);
        exception.ProblemDetails?.Title
            .Should().Be("The entity with specified Expiry and Production dates cannot be created");

    }
    
    [Fact(DisplayName = "BoxOversizeException")]
    public async Task Create_ShouldReturnError_IfBoxBiggerThanPalette()
    {
        // Arrange
        var warehouseId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        var boxId = Guid.NewGuid();
        var boxRequest = new BoxRequest
        {
            Width = 15, Depth = 15, Height = 15,
            Weight = 1,
            ProductionDate = new DateTime(2007,1,1, 0,0,0,0, DateTimeKind.Utc),
            ExpiryDate = new DateTime(2008,1,1, 0,0,0,0, DateTimeKind.Utc)
        };
        
        await GenerateWarehouse(warehouseId);
        await GeneratePalette(warehouseId, paletteId);

        // Act
        async Task Act() => await Sut.BoxClient.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);
    
        // Assert
        exception.Message.Should().Be("API request failed!");
        exception.ProblemDetails?.Status.Should().Be(400);
        exception.ErrorCode.Should().Be("incorrect_http_request");
        exception.ProblemDetails?.Type.Should().Be("unit_oversize");
        exception.ProblemDetails?.Title.Should().Be("The box does not match the dimensions of the pallet");
    }
}
