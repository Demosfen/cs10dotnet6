using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Store.Postgres.DI;

namespace Wms.Web.Store.Postgres;

public sealed class PostgresDbContextModule : Module
{
    private static readonly string ConnectionStringName = "WmsPgsql";

    protected override void Load(ContainerBuilder containerBuilder)
    {
        var services = new ServiceCollection();
        services.AddDbContextPool<WarehouseDbContext>((provider, builder) =>
        {
            var connectionString = provider
                .GetRequiredService<IConfiguration>()
                .GetConnectionString(ConnectionStringName);
            builder.UseNpgsql(
                connectionString, 
                optionsBuilder => { optionsBuilder.EnableRetryOnFailure(); });
        });
        containerBuilder.Populate(services);

        containerBuilder
            .Register(context => context.Resolve<WarehouseDbContext>())
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        containerBuilder.RegisterModule<RepositoriesModule>();
    }
}