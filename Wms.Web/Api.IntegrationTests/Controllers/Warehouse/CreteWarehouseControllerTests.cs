using FluentAssertions;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Wms.Web.Common.Exceptions;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms.WarehouseControllerTests;

public sealed class CreteWarehouseControllerTests : TestControllerBase
{
    private readonly IWarehouseClient _sut;

    public CreteWarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        _sut = new WarehouseClient(HttpClient);
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
        var createWarehouse = await _sut.CreateAsync(id, request, CancellationToken.None);

        // Assert
        createWarehouse.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "EmptyWarehouseName")]
    public async Task Create_ReturnsValidatorError_WhenNameIsNull()
    {
        // Arrange
        var id = Guid.NewGuid();
    
        // Act
        async Task Act() => await _sut.CreateAsync(id, new WarehouseRequest { Name = "" });

        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);
        exception.ProblemDetails?.Errors!.ContainsKey("Name").Should().BeTrue();
        exception.ProblemDetails?.Errors!["Name"].Should().Contain("Name of the warehouse should not be null or empty.");
        exception.Message.Should().Be("API request failed!");
    }

    [Fact(DisplayName = "WarehouseNameTooLong")]
    public async Task Create_ReturnsValidatorError_WhenNameGreaterThan40Characters()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        async Task Act() => await _sut
            .CreateAsync(id, new WarehouseRequest { Name = Guid.NewGuid().ToString() + Guid.NewGuid() });

        var exception = await Assert.ThrowsAsync<ApiValidationException>(Act);
        exception.ProblemDetails?.Errors!.ContainsKey("Name.Length").Should().BeTrue();
        exception.ProblemDetails?.Errors!["Name.Length"]
            .Should().Contain("Warehouse name sholuld be less than or equal to 40 characters");
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
        await _sut.CreateAsync(id, request);
        async Task Act() => await _sut.CreateAsync(id, request);
        var exception = await Assert.ThrowsAsync<EntityAlreadyExistException>(Act);
        exception.Id.Should().Be(id);
        exception.Message.Should().Be($"The entity with id={id} already exist");
    }
}
