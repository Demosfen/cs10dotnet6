using Xunit;

namespace Wms.Web.Api.ContainerTests.Abstract;

[Collection(ContainerTestCollection.Name)]
public abstract class TestControllerBase : IAsyncLifetime
{
    protected readonly HttpClient HttpClient;

    protected TestControllerBase(TestApplication apiFactory)
    {
        HttpClient = apiFactory.HttpClient;
    }
    
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
}