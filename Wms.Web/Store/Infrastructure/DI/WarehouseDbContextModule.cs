using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wms.Web.Store.Infrastructure.DI;

public sealed class WarehouseDbContextModule : Module
{
    public static readonly string ConnectionString = "DataSource=../warehouse.db";

    protected override void Load(ContainerBuilder containerBuilder)
    {
        var services = new ServiceCollection();
        services.AddEntityFrameworkSqlite();
        services.AddDbContextPool<WarehouseDbContext>((provider, builder) =>
        {
            var connectionString = provider
                .GetRequiredService<IConfiguration>()
                .GetConnectionString(ConnectionString);
            builder.UseSqlite(connectionString);
            builder.UseInternalServiceProvider(provider);
        });
        containerBuilder.Populate(services);

        containerBuilder
            .Register(context => context.Resolve<WarehouseDbContext>())
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}