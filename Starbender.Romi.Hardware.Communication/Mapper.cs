namespace Starbender.Romi.Hardware.Communication
{
    using AutoMapper;

    using Unosquare.RaspberryIO.Gpio;

    public class Mapper : Profile
    {
        private static GpioController gpio = GpioController.Instance;

        public Mapper()
        {
            // CreateMap<WiringPiPin, GpioPin>().ConvertUsing((wPin,gPin) => new GpioPin(wPin,gpio.HeaderP1));
        }
    }
}