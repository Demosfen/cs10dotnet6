using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Wms.Web.Store.Sqlite.DI;

namespace Wms.Web.Store.Sqlite;

public sealed class SqliteDbContextModule : Module
{
    private static readonly string ConnectionStringName = "WmsSqlite";

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
            .AsSelf()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        containerBuilder.RegisterModule<RepositoriesModule>();
    }
}