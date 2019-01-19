namespace Starbender.Services.Gpio.WiringPi.Model
{
    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.WiringPi;
    using Unosquare.WiringPi.Native;

    /// <summary>
    /// Represents a GPIO Pin, its location and its capabilities.
    /// Full pin reference available here:
    /// http://pinout.xyz/pinout/pin31_gpio6 and  http://wiringpi.com/pins/.
    /// </summary>
    public sealed class GpioPinModel : IGpioPinModel
    {
        public BcmPin BcmPin { get; set; }

        public int BcmPinNumber { get; set; }

        public PinCapability Capabilities { get; set; }

        public GpioHeader Header { get; set; }

        public GpioPinResistorPullMode InputPullMode { get; set; }

        public InterruptServiceRoutineCallback InterruptCallback { get; set; }

        public EdgeDetection InterruptEdgeDetection { get; set; }

        public bool IsInSoftPwmMode { get; set; }

        public bool IsInSoftToneMode { get; set; }

        public string Name { get; set; }

        public int PhysicalPinNumber { get; set; }

        public GpioPinDriveMode PinMode { get; set; }

        public int PwmClockDivisor { get; set; }

        public PwmMode PwmMode { get; set; }

        public uint PwmRange { get; set; }

        public int PwmRegister { get; set; }

        public int SoftPwmRange { get; set; }

        public int SoftPwmValue { get; set; }

        public int SoftToneFrequency { get; set; }

        public bool Value { get; set; }

        public WiringPiPin WiringPiPinNumber { get; set; }
    }
}