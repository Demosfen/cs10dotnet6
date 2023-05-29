using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Wms.Web.Api.UnitTests.DI;

/// <summary>
/// Fixture for DI-container tests
/// </summary>
public sealed class ContainerFixture : WebApplicationFactory<IApiMarker>
{
    public IServiceProvider Container => Services;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder) 
        => builder.UseSetting("SkipMigration", "true");
}