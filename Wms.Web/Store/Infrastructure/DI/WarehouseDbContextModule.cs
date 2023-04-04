using Autofac;
using Microsoft.EntityFrameworkCore;

namespace Wms.Web.Store.Infrastructure.DI;

public sealed class WarehouseDbContextModule : Module
{
    public static readonly string ConnectionString = "DataSource=../warehouse.db";

    protected override void Load(ContainerBuilder containerBuilder)
    {
        containerBuilder.Register(c =>
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseSqlite(ConnectionString)
                .Options;
            return new WarehouseDbContext(options);
        }).InstancePerLifetimeScope();
    }
}