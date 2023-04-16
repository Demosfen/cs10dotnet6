using Autofac;
using Autofac.Extensions.DependencyInjection;
using Wms.Web.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Wms.Web.Services.Infrastructure.DI;

public sealed class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var serviceCollection = new ServiceCollection();
        builder.Populate(serviceCollection);
        
        builder
            .RegisterAssemblyTypes(ThisAssembly)
            .AssignableTo<IBusinessService>()
            .AsSelf()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}