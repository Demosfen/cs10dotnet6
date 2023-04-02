using Autofac;
using Wms.Web.Repositories.Abstract;

namespace Wms.Web.Repositories.Infrastructure.DI;

public sealed class RepositoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterAssemblyTypes(ThisAssembly)
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}