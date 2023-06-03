using Autofac;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Store.Sqlite.Repositories;

namespace Wms.Web.Store.Sqlite.DI;

public sealed class RepositoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>))
            .As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();
    }
}