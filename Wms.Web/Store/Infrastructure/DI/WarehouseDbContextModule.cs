using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Wms.Web.Store.Infrastructure.DI;

public sealed class WarehouseDbContextModule : Module
{
    private static readonly string ConnectionStringName = "WarehouseDbContextCS";

    protected override void Load(ContainerBuilder containerBuilder)
    {
        containerBuilder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var connectionString = config.GetConnectionString(ConnectionStringName);

                var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                    .UseSqlite(connectionString)
                    .Options;
                
                return new WarehouseDbContext(options);
            })
            .AsSelf()           //TODO needed when create new Migration https://github.com/Demosfen/cs10dotnet6/pull/6#discussion_r1161709855
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}