using Autofac;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Repositories.Concrete;
using Wms.Web.Store.Entities;

namespace Wms.Web.Repositories.Infrastructure.DI;

public sealed class RepositoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterAssemblyTypes(ThisAssembly)
            .AssignableTo<IRepository>()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}