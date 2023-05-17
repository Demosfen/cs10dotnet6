using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Wms.Web.Api.IntegrationTests.Extensions;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms.BoxControllerTests;

public sealed class CreteBoxControllerTests : TestControllerBase
{
    private readonly BoxClient _sut;
    
    public CreteBoxControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri(BaseUri)
        });
        
        _sut = new BoxClient(HttpClient, options);

        // _dataHelper = new WmsDataHelper(apiFactory);
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
            Weight = 1, ExpiryDate = new DateTime(2007, 1, 1)
        };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);

        // Act
        var createBox = await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        var createdBox = await createBox.Content.ReadFromJsonAsync<BoxRequest>();

        // Assert
        createBox.StatusCode.Should().Be(HttpStatusCode.Created);
        createdBox.Should().BeEquivalentTo(boxRequest);
        createBox.Headers.Location
            .Should().Be($"{BaseUri}{Ver1}boxes/{boxId.ToString()}");
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
        var createBox = await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
        var createCloneBox = await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);

        // Assert
        createBox.StatusCode.Should().Be(HttpStatusCode.Created);
        createCloneBox.StatusCode.Should().Be(HttpStatusCode.Conflict);
        var error = await createCloneBox.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error!.Status.Should().Be(409);
        error.Type.Should().Be("entity_already_exist");
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
        var createBox = await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
    
        // Assert
        createBox.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var error = await createBox.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error!.Status.Should().Be(400);
        error.Errors.Count.Should().Be(3);
        error.Errors.Should().ContainKey("Depth", "'Depth' must not be empty.");
        error.Errors.Should().ContainKey("Depth", "Box depth should not be zero or negative.");
        error.Errors.Should().ContainKey("Width", "Box width should not be zero or negative.");
        error.Errors.Should().ContainKey("Height", "Box height too big");
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
        var createBox = await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);
    
        // Assert
        createBox.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        var error = await createBox.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error!.Status.Should().Be(422);
        error.Type.Should().Be("entity_expiry_incorrect");
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
        var createBox = await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);

        // Assert
        createBox.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        var error = await createBox.Content.ReadFromJsonAsync<ProblemDetails>();
        error!.Status.Should().Be(500);
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
            Width = 15, Depth = 155, Height = 15,
            Weight = 1,
            ProductionDate = new DateTime(2007,1,1),
            ExpiryDate = new DateTime(2008,1,1)
        };
        
        await DataHelper.GenerateWarehouse(warehouseId);
        await DataHelper
            .GeneratePalette(warehouseId, paletteId, paletteRequest);

        // Act
        var createBox = await _sut.CreateAsync(paletteId, boxId, boxRequest, CancellationToken.None);

        // Assert
        createBox.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        var error = await createBox.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error!.Status.Should().Be(422);
        error.Type.Should().Be("unit_oversize");
    }
}
