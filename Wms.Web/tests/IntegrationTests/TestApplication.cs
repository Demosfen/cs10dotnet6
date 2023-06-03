using Microsoft.AspNetCore.Mvc.Testing;
using Wms.Web.Api;
using Wms.Web.Client.Custom.Abstract;
using Wms.Web.Client.Custom.Concrete;
using Wms.Web.IntegrationTests.Infrastructure;
using Wms.Web.Store.Common.Interfaces;
using Xunit;

namespace Wms.Web.IntegrationTests;

public sealed class TestApplication : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly TestDatabaseFixture _dbFixture = new();
    
    private HttpClient? _httpClient;
    protected IWmsClient? _wmsClient;

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