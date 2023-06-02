using Autofac;
using Wms.Web.Repositories.Concrete;
using Wms.Web.Repositories.Interfaces.Interfaces;

namespace Wms.Web.Repositories.DI;

public sealed class RepositoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>))
            .As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();
    }
}