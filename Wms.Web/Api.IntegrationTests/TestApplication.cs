using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Api.IntegrationTests.Infrastructure;
using Wms.Web.Store;
using Xunit;

namespace Wms.Web.Api.IntegrationTests;

public sealed class TestApplication : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly TestDatabaseFixture _dbFixture = new();
    
    private HttpClient? _httpClient;

    public HttpClient HttpClient => _httpClient ?? throw new InvalidOperationException("No httpClient");
    
    protected override void ConfigureWebHost(IWebHostBuilder builder) 
        => builder.UseSetting("ConnectionStrings:WarehouseDbContextCS", 
            _dbFixture.GetConnectionString());
    
    public Task InitializeAsync()
    {
        _httpClient = CreateClient();
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        _dbFixture.Dispose();
        return Task.CompletedTask;
    }
}