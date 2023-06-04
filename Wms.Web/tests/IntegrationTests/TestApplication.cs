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
    
    private IWmsClient? _wmsClient;

    public IWmsClient WmsClient => _wmsClient ?? throw new InvalidOperationException("No WmsClient");

    public IWarehouseDbContext CreateDbContext() => Services.GetRequiredService<IWarehouseDbContext>();

    protected override void ConfigureWebHost(IWebHostBuilder builder) 
        => builder.UseSetting("ConnectionStrings:WarehouseDbContextCS", 
            _dbFixture.GetConnectionString());
    
    public Task InitializeAsync()
    {
        var httpClient = CreateClient();
        
        _wmsClient = new WmsClient(
            new WarehouseClient(httpClient),
            new PaletteClient(httpClient),
            new BoxClient(httpClient));
        
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        _dbFixture.Dispose();
        return Task.CompletedTask;
    }
}