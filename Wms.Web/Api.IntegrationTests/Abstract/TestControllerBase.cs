using Wms.Web.Api.IntegrationTests.Extensions;
using Wms.Web.Store.Interfaces;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Abstract;

[Collection(IntegrationTestCollection.Name)]
public abstract partial class TestControllerBase : IAsyncLifetime
{
    protected readonly HttpClient HttpClient;
    
    protected IWarehouseDbContext DbContext { get; }
    
    internal readonly DataHelper DataHelper;
    internal const string Ver1 = "/api/v1/";
    internal const string BaseUri = "http://localhost";
    
    protected TestControllerBase(TestApplication apiFactory)
    {
        HttpClient = apiFactory.HttpClient;
        DbContext = apiFactory.CreateDbContext();
        DataHelper = new DataHelper(DbContext);
    }
    
    public Task InitializeAsync() => Task.CompletedTask;
    
    public Task DisposeAsync()
    {
        DbContext.Dispose();
        return Task.CompletedTask;
    }
}