using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Wms.Web.Business.Abstract;

namespace Wms.Web.Business.Infrastructure.DI;

public sealed class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var serviceCollection = new ServiceCollection();
        builder.Populate(serviceCollection);
        
        builder
            .RegisterAssemblyTypes(ThisAssembly)
            .AssignableTo<IBusinessService>()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}