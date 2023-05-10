using Xunit;

namespace Wms.Web.Api.IntegrationTests.Abstract;

[Collection(IntegrationTestCollection.Name)]
public abstract class TestControllerBase : IAsyncLifetime
{
    protected readonly HttpClient HttpClient;
    // protected readonly IOptions<WmsClientOptions> Options;

    protected TestControllerBase(TestApplication apiFactory)
    {
        HttpClient = apiFactory.HttpClient;
    }
    
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
}