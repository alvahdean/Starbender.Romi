namespace Starbender.Romi.WebApi.Config
{
    using System.Reflection;

    using Autofac;

    using NodaTime;

    using Module = Autofac.Module;

    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(
                    x => x.IsClass && !x.IsAbstract
                                   && x.Name.StartsWith("Starbender")
                                   && (x.Name.EndsWith("Provider") || x.Name.EndsWith("Service")
                                                                   || x.Name.EndsWith("Factory")))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}