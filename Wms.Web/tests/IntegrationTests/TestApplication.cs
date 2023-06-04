using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;
using Wms.Web.Api;
using Wms.Web.Client.Custom.Abstract;
using Wms.Web.Client.Custom.Concrete;
using Wms.Web.Store.Common.Interfaces;
using Xunit;

namespace Wms.Web.IntegrationTests;

public sealed class TestApplication : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithImage("postgres:alpine3.18")
            .WithDatabase("TestDatabase")
            .WithUsername("user")
            .WithPassword("password").Build();

    
    private IWmsClient? _wmsClient;

    public IWmsClient WmsClient => _wmsClient ?? throw new InvalidOperationException("No WmsClient");

    public IWarehouseDbContext CreateDbContext() => Services.GetRequiredService<IWarehouseDbContext>();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:WmsPgsql",
            _dbContainer.GetConnectionString());
        
        builder.UseSetting("Wms:DbProvider", "Postgres");
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        
        var httpClient = CreateClient();
        
        _wmsClient = new WmsClient(
            new WarehouseClient(httpClient),
            new PaletteClient(httpClient),
            new BoxClient(httpClient));
    }

    public new async Task DisposeAsync()
        => await _dbContainer.StopAsync();
}