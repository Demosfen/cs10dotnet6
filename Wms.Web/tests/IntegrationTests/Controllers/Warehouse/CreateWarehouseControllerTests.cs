using FluentAssertions;
using Wms.Web.Common.Exceptions;
using Wms.Web.Contracts.Requests;
using Wms.Web.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.IntegrationTests.Controllers.Warehouse;

public sealed class CreateWarehouseControllerTests : TestControllerBase
{
    public CreateWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
    }
    
    [Fact(DisplayName = "CreateWarehouse")]
    public async Task Create_ShouldCreateWarehouse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new WarehouseRequest
        {
            Name = id.ToString()
        };
        
        // Act
        var createWarehouse = await Sut.WarehouseClient
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
        async Task Act() => await Sut.WarehouseClient
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
        async Task Act() => await Sut.WarehouseClient
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
        await Sut.WarehouseClient.CreateAsync(id, request);
        async Task Act() => await Sut.WarehouseClient.CreateAsync(id, request);
        var exception = await Assert.ThrowsAsync<EntityAlreadyExistException>(Act);
        
        // Assert
        exception.Id.Should().Be(id);
        exception.Message.Should().Be($"The entity with id={id} already exist");
    }
}
