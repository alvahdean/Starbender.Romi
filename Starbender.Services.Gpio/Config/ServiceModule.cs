namespace Starbender.Services.Gpio.Config
{
    using System.Reflection;

    using Autofac;
    using Unosquare.RaspberryIO;
    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.Swan.Components;
    using Unosquare.WiringPi;

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
            builder.RegisterInstance<IGpioController>(Unosquare.RaspberryIO.Pi.GpioController);
            BootstrapWiringPi piBootstrap=new BootstrapWiringPi();
            piBootstrap.Bootstrap();
        }
    }
}
