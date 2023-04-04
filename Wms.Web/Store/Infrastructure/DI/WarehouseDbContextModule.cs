using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wms.Web.Store.Infrastructure.DI;

public sealed class WarehouseDbContextModule : Module
{
    public static readonly string ConnectionStringName = "WarehouseDbContextCS";

    protected override void Load(ContainerBuilder containerBuilder)
    {
        containerBuilder.Register(c =>
            {
                var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                    .UseSqlite(ConnectionStringName)
                    .Options;
                return new WarehouseDbContext(options);
            })
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}