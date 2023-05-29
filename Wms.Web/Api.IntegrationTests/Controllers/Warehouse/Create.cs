using FluentAssertions;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Wms.Web.Common.Exceptions;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Controllers.Warehouse;

public sealed class Create : TestControllerBase
{
    private readonly IWmsClient _sut;

    public Create(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WmsClient(
            new WarehouseClient(apiFactory.HttpClient),
            new PaletteClient(apiFactory.HttpClient),
            new BoxClient(apiFactory.HttpClient));    
    }
    
    [Fact(DisplayName = "CreateWarehouse")]
    public async Task Create_ShouldCreateWarehouse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new WarehouseRequest
        {
            Name = "Warehouse#Create"
        };
        
        // Act
        var createWarehouse = await _sut.WarehouseClient
            .CreateAsync(id, request, CancellationToken.None);

        // Assert
        createWarehouse.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "EmptyWarehouseName")]
    public async Task Create_ReturnsValidatorError_WhenNameIsNull()
    {
        // Arrange
        var id = Guid.NewGuid();
    
        // Act
        async Task Act() => await _sut.WarehouseClient
            .CreateAsync(id, new WarehouseRequest { Name = "" });
        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);
        
        // Assert
        exception.ProblemDetails?.Errors!["Name"].Should().Contain("Name of the warehouse should not be null or empty.");
        exception.Message.Should().Be("API request failed!");
    }

    [Fact(DisplayName = "WarehouseNameTooLong")]
    public async Task Create_ReturnsValidatorError_WhenNameGreaterThan40Characters()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        async Task Act() => await _sut.WarehouseClient
            .CreateAsync(id, new WarehouseRequest { Name = Guid.NewGuid().ToString() + Guid.NewGuid() });
        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);
        
        // Assert
        exception.ProblemDetails?.Errors!["Name.Length"]
            .Should().Contain("Warehouse name should be less than or equal to 40 characters");
        exception.Message.Should().Be("API request failed!");
    }
    
    [Fact(DisplayName = "WarehouseConflict")]
    public async Task Create_ReturnsError_WhenWarehouseAlreadyExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new WarehouseRequest
        {
            Name = "Warehouse#Conflict"
        };
        
        // Act
        await _sut.WarehouseClient.CreateAsync(id, request);
        async Task Act() => await _sut.WarehouseClient.CreateAsync(id, request);
        var exception = await Assert.ThrowsAsync<EntityAlreadyExistException>(Act);
        
        // Assert
        exception.Id.Should().Be(id);
        exception.Message.Should().Be($"The entity with id={id} already exist");
    }
}
