using Wms.Web.Api.IntegrationTests.Extensions;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Abstract;

[Collection(IntegrationTestCollection.Name)]
public abstract class TestControllerBase : IAsyncLifetime
{
    protected readonly HttpClient HttpClient;
    
    internal readonly WmsDataHelper DataHelper;
    internal const string Ver1 = "/api/v1/";
    internal const string BaseUri = "http://localhost";

    protected TestControllerBase(TestApplication apiFactory)
    {
        HttpClient = apiFactory.HttpClient;
        
        DataHelper = new WmsDataHelper(apiFactory);
    }
    
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
}