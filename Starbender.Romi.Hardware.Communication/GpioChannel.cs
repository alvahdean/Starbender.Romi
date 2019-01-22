namespace Starbender.Romi.Hardware.Communication
{
    using Unosquare.RaspberryIO.Gpio;

    public class GpioChannel
    {
        public GpioMode Mode { get; set; } = GpioMode.Undefined;

        public WiringPiPin Pin { get; set; } = WiringPiPin.Unknown;
    }
}