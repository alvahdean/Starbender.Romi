namespace Starbender.Services.RaspberryPi.Models.Gpio
{
    public interface IGpioPinModel
    {
        BcmPinEnum BcmPin { get; set; }

        int BcmPinNumber { get; set; }

        PinCapabilityEnum Capabilities { get; set; }

        GpioHeaderEnum Header { get; set; }

        GpioPinResistorPullModeEnum InputPullMode { get; set; }

        EdgeDetectionEnum InterruptEdgeDetection { get; set; }

        bool IsInSoftPwmMode { get; set; }

        bool IsInSoftToneMode { get; set; }

        string Name { get; set; }

        int PhysicalPinNumber { get; set; }

        GpioPinDriveModeEnum PinMode { get; set; }

        int PwmClockDivisor { get; set; }

        PwmModeEnum PwmMode { get; set; }

        uint PwmRange { get; set; }

        int PwmRegister { get; set; }

        int SoftPwmRange { get; }

        int SoftPwmValue { get; set; }

        int SoftToneFrequency { get; set; }

        bool Value { get; set; }

        WiringPiPinEnum WiringPiPinNumber { get; set; }
    }
}