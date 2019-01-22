namespace Starbender.Services.RaspberryPi.Models.Gpio
{
    /// <summary>
    /// Represents a GPIO Pin, its location and its capabilities.
    /// Full pin reference available here:
    /// http://pinout.xyz/pinout/pin31_gpio6 and  http://wiringpi.com/pins/.
    /// </summary>
    public class GpioPinModel : IGpioPinModel
    {
        public BcmPinEnum BcmPin { get; set; }

        public int BcmPinNumber { get; set; }

        public PinCapabilityEnum Capabilities { get; set; }

        public GpioHeaderEnum Header { get; set; }

        public GpioPinResistorPullModeEnum InputPullMode { get; set; }

        public EdgeDetectionEnum InterruptEdgeDetection { get; set; }

        public bool IsInSoftPwmMode { get; set; }

        public bool IsInSoftToneMode { get; set; }

        public string Name { get; set; }

        public int PhysicalPinNumber { get; set; }

        public GpioPinDriveModeEnum PinMode { get; set; }

        public int PwmClockDivisor { get; set; }

        public PwmModeEnum PwmMode { get; set; }

        public uint PwmRange { get; set; }

        public int PwmRegister { get; set; }

        public int SoftPwmRange { get; set; }

        public int SoftPwmValue { get; set; }

        public int SoftToneFrequency { get; set; }

        public bool Value { get; set; }

        public WiringPiPinEnum WiringPiPinNumber { get; set; }
    }
}