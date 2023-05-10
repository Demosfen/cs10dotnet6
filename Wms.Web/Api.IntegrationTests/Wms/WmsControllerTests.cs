using FluentAssertions;
using Microsoft.Extensions.Options;
using Wms.Web.Api.Client;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Api.IntegrationTests.Abstract;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Wms;

public sealed class WarehouseControllerTests : TestControllerBase
{
    private readonly IWarehouseClient _sut;

    public WarehouseControllerTests(TestApplication apiFactory) 
        : base(apiFactory)
    {
        var options = Options.Create(new WmsClientOptions
        {
            HostUri = new Uri("http://localhost:5000")
        });
        
        _sut = new WarehouseClient(HttpClient, options);
    }
    
    [Fact(DisplayName = "...")]
    public async Task WarehouseController_CreateWarehouse_ShouldCreateWarehouse()
    {
        var result = await _sut.PostAsync(Guid.NewGuid(), new WarehouseRequest
        {
            Name = "TestWarehouse"
        }, 
            CancellationToken.None);

        result.Should().NotBeNull();

    } 
}
