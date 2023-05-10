using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Store.Interfaces;
using Xunit;

namespace Wms.Web.Api.IntegrationTests;

public sealed class TestApplication : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private DbConnection? _dbConnection;
    private HttpClient? _httpClient;

    private DbConnection DbConnection => _dbConnection ?? throw new InvalidOperationException("No dbConnection");
    public HttpClient HttpClient => _httpClient  ?? throw new InvalidOperationException("No HttpClient");
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Use the in-memory database connection string
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<WarehouseDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<PopulationDbContext>(options =>
            {
                options.UseSqlite(DbConnection);
            });
        });
    }
    
    public async Task InitializeAsync()
    {
        _httpClient = CreateClient();
    }
    
    public new async Task DisposeAsync()
        => await _dbContainer.StopAsync();
}