using Autofac;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Store.Postgres.Repositories;

namespace Wms.Web.Store.Postgres.DI;

public sealed class RepositoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>))
            .As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();
    }
}