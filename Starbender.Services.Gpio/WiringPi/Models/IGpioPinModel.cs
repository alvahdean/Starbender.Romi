namespace Starbender.Services.Gpio.WiringPi.Model
{
    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.WiringPi;
    using Unosquare.WiringPi.Native;

    public interface IGpioPinModel
    {
        string Name { get; set; }

        BcmPin BcmPin { get; set; }

        int BcmPinNumber { get; set; }

        PinCapability Capabilities { get; set; }

        GpioHeader Header { get; set; }

        GpioPinResistorPullMode InputPullMode { get; set; }

        InterruptServiceRoutineCallback InterruptCallback { get; set; }

        EdgeDetection InterruptEdgeDetection { get; set; }

        bool IsInSoftPwmMode { get; set; }

        bool IsInSoftToneMode { get; set; }

        int PhysicalPinNumber { get; set; }

        GpioPinDriveMode PinMode { get; set; }

        int PwmClockDivisor { get; set; }

        PwmMode PwmMode { get; set; }

        uint PwmRange { get; set; }

        int PwmRegister { get; set; }

        int SoftPwmRange { get; }

        int SoftPwmValue { get; set; }

        int SoftToneFrequency { get; set; }

        bool Value { get; set; }

        WiringPiPin WiringPiPinNumber { get; set; }
    }
}