using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Api.Client.Custom.Abstract;
using Wms.Web.Api.Client.Custom.Concrete;
using Wms.Web.Api.IntegrationTests.Infrastructure;
using Wms.Web.Store;
using Wms.Web.Store.Interfaces;
using Xunit;

namespace Wms.Web.Api.IntegrationTests;

public sealed class TestApplication : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly TestDatabaseFixture _dbFixture = new();
    
    private HttpClient? _httpClient;
    private IWmsClient? _wmsClient;

    public HttpClient HttpClient => _httpClient ?? throw new InvalidOperationException("No httpClient");
    
    public IWmsClient WmsClient => _wmsClient ?? throw new InvalidOperationException("No httpClient");

    public IWarehouseDbContext CreateDbContext() => Services.GetRequiredService<IWarehouseDbContext>();

    protected override void ConfigureWebHost(IWebHostBuilder builder) 
        => builder.UseSetting("ConnectionStrings:WarehouseDbContextCS", 
            _dbFixture.GetConnectionString());
    
    public Task InitializeAsync()
    {
        _httpClient = CreateClient();
        _wmsClient = new WmsClient(
            new WarehouseClient(HttpClient),
            new PaletteClient(HttpClient),
            new BoxClient(HttpClient));
        
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        _dbFixture.Dispose();
        return Task.CompletedTask;
    }
}