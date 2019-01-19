namespace Starbender.Romi.Web.Service.Module
{
    using System.Reflection;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Module = Autofac.Module;

    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterApiControllers(thisAssembly);

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.IsClass && !x.IsAbstract && (x.Name.EndsWith("Provider") || x.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
