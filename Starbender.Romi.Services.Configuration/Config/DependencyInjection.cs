namespace Starbender.Romi.Services.Configuration
{
    using System.Reflection;

    using Autofac;

    using NodaTime;

    using Starbender.Core.Time;

    using Module = Autofac.Module;

    public class DependencyInjection : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(
                    x => x.IsClass && !x.IsAbstract
                                   && (x.Name.EndsWith("Provider") || x.Name.EndsWith("Service")
                                                                   || x.Name.EndsWith("Factory")))
                .AsImplementedInterfaces()
                .SingleInstance();
            IDateTimeZoneProvider tzProvider = CurrentTzdbProvider.Load();
            builder.RegisterInstance<IDateTimeZoneProvider>(tzProvider);
        }
    }
}