using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Wms.Web.Store.Common;

namespace Wms.Web.Store.Postgress;

public sealed class PostgressDbContextModule : Module
{
    private static readonly string ConnectionStringName = "WarehouseDbContextCS";

    protected override void Load(ContainerBuilder containerBuilder)
    {
        containerBuilder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var connectionString = config.GetConnectionString(ConnectionStringName);

                var options = ((DbContextOptionsBuilder)new DbContextOptionsBuilder<WarehouseDbContext>()
                    .UseNpgsql(connectionString)).Options;
                
                return new WarehouseDbContext(options);
            })
            .AsSelf()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}