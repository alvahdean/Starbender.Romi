namespace Starbender.Romi.Web.Service.Models
{
    public sealed class UpdateGpioPinRequest
    {
        public int BcmPin { get; set; }

        public int BcmPinNumber { get; set; }

        public int Capabilities { get; set; }

        public int Header { get; set; }

        public int InputPullMode { get; set; }

        public int InterruptCallback { get; set; }

        public int InterruptEdgeDetection { get; set; }

        public bool IsInSoftPwmMode { get; set; }

        public bool IsInSoftToneMode { get; set; }

        public string Name { get; set; }

        public int PhysicalPinNumber { get; set; }

        public int PinMode { get; set; }

        public int PwmClockDivisor { get; set; }

        public int PwmMode { get; set; }

        public uint PwmRange { get; set; }

        public int PwmRegister { get; set; }

        public int SoftPwmRange { get; set; }

        public int SoftPwmValue { get; set; }

        public int SoftToneFrequency { get; set; }

        public bool Value { get; set; }

        public int WiringPiPinNumber { get; set; }
    }
}