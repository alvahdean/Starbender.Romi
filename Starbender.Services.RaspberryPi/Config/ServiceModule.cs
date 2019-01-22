namespace Starbender.Services.RaspberryPi.Config
{
    using System.Reflection;

    using Autofac;

    using Unosquare.RaspberryIO;
    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.WiringPi;

    using GpioController = Unosquare.RaspberryIO.Gpio.GpioController;
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

            // Don't register hardware devices
            ////builder.RegisterInstance<GpioController>(Pi.Gpio);
            ////builder.RegisterInstance<ISpiBus>(new SpiBus());
            ////builder.RegisterInstance<II2CBus>(new I2CBus());
            ////builder.RegisterInstance<ISystemInfo>(new SystemInfo());
            ////builder.RegisterInstance<ITiming>(new Timing());
            ////builder.RegisterInstance<IThreading>(new Threading());
        }
    }
}
