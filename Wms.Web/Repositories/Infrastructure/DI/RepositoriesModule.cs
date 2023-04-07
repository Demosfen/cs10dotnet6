using Autofac;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Repositories.Concrete;

namespace Wms.Web.Repositories.Infrastructure.DI;

public sealed class RepositoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterAssemblyTypes(ThisAssembly)
            // .IfNotRegistered(typeof(WarehouseRepository)).As<IWarehouseRepository>()
            .AssignableTo<IRepository>()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}