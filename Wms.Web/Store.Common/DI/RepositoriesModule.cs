using Autofac;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Repositories.Interfaces.Interfaces;
using Wms.Web.Store.Common.Repositories;

namespace Wms.Web.Store.Common.DI;

public sealed class RepositoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>))
            .As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();
    }
}