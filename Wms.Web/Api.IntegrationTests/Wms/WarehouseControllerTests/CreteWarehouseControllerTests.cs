using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.Contracts.Responses;
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
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri(BaseUri)
        });
        
        _sut = new WarehouseClient(HttpClient, options);
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
        var response = await _sut.CreateAsync(id, request, CancellationToken.None);

        // Assert
        response.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "EmptyWarehouseName")]
    public async Task Create_ReturnsValidatorError_WhenNameIsNull()
    {
        // Arrange
        var id = Guid.NewGuid();
    
        // Act
        Func<Task> act = async () => await _sut.CreateAsync(id, new WarehouseRequest { Name = "" });

        // Assert
        await act.Should().ThrowAsync<UninitializedPropertyException>();
    }
    
    // [Fact(DisplayName = "WarehouseNameTooLong")]
    // public async Task Create_ReturnsValidatorError_WhenNameGreaterThan25Characters()
    // {
    //     // Arrange
    //     var id = Guid.NewGuid();
    //
    //     // Act
    //     var response = await _sut.CreateAsync(id, new WarehouseRequest{Name = "Warehouse-" + Guid.NewGuid()});
    //     
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //     var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    //     error!.Status.Should().Be(400);
    //     error.Errors.Should().ContainKey("Name.Length"); 
    // }
    //
    // [Fact(DisplayName = "WarehouseConflict")]
    // public async Task Create_ReturnsError_WhenWarehouseAlreadyExist()
    // {
    //     // Arrange
    //     var id = Guid.NewGuid();
    //     var request = new WarehouseRequest
    //     {
    //         Name = "Warehouse#Conflict"
    //     };
    //     
    //     // Act
    //     var createResponse = await _sut.CreateAsync(id, request);
    //     var response = await _sut.CreateAsync(id, request);
    //     
    //     // Assert
    //     createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
    //     response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    //     var error = response.Content.ReadFromJsonAsync<ProblemDetails>();
    //     error.Result?.Type.Should().Be("entity_already_exist");
    //     error.Result?.Status.Should().Be(409);
    // }
}
