namespace Starbender.Romi.Services.Configuration
{
    using System.Reflection;

    using Autofac;

    using Module = Autofac.Module;

    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(
                    x => x.IsClass && !x.IsAbstract
                                   && (x.Name.EndsWith("Provider") || x.Name.EndsWith("Service")
                                                                   || x.Name.EndsWith("Factory")))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}