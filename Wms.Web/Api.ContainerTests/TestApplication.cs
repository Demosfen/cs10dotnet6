using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;
using Npgsql;
using Xunit;

namespace Wms.Web.Api.ContainerTests;

public sealed class TestApplication :
    WebApplicationFactory<IApiMarker>, 
    IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = 
        new PostgreSqlBuilder()
            .WithDatabase("TestDb")
            .WithUsername("user")
            .WithPassword("password").Build();
    
    private DbConnection? _dbConnection;
    private HttpClient? _httpClient;

    private DbConnection DbConnection => _dbConnection ?? throw new InvalidOperationException("No dbConnection");
    public HttpClient HttpClient => _httpClient  ?? throw new InvalidOperationException("No HttpClient");
    
    protected override void ConfigureWebHost(IWebHostBuilder builder) 
        => builder.UseSetting("ConnectionStrings:PopulationDbContext", _dbContainer.GetConnectionString());

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        _httpClient = CreateClient();
    }

    public new async Task DisposeAsync() 
        => await _dbContainer.StopAsync();
}