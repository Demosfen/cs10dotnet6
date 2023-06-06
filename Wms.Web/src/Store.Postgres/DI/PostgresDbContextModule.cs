using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wms.Web.Store.Postgres.DI;

public sealed class PostgresDbContextModule : Module
{
    public static readonly string ConnectionStringName = "WmsPgsql";

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