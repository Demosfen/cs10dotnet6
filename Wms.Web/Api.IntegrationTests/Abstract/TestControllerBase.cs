using AutoMapper;
using Wms.Web.Services.Infrastructure.Mapping;
using Wms.Web.Store.Interfaces;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Abstract;

[Collection(IntegrationTestCollection.Name)]
public abstract partial class TestControllerBase : IAsyncLifetime
{
    protected readonly HttpClient HttpClient;
    
    protected IWarehouseDbContext DbContext { get; }
    
    internal const string BaseUri = "http://localhost";
    
    protected TestControllerBase(TestApplication apiFactory)
    {
        HttpClient = apiFactory.HttpClient;
        DbContext = apiFactory.CreateDbContext();
        var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(DtoEntitiesMappingProfile));
            });

        Mapper = configurationProvider.CreateMapper();
    }
    
    public Task InitializeAsync() => Task.CompletedTask;
    
    public Task DisposeAsync()
    {
        // DbContext.Dispose(); //TODO shouldn't be here, or the error occurs
        return Task.CompletedTask;
    }
}