using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.ContainerTests.Abstract;
using Xunit;

namespace Wms.Web.Api.ContainerTests.Wms;

public sealed class WmsControllerTests : TestControllerBase
{
    private readonly IWmsClient _sut;
    
    public WmsControllerTests(TestApplication apiFactory) : base(apiFactory)
    {
        _sut = new WmsClient();
    }

    [Fact(DisplayName = "...")]
    public async Task BoxController_GetNotDeletedAsync_ShouldReturnUndeletedBoxList()
    {
        
    } 
}