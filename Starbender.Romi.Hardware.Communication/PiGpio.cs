namespace Starbender.Romi.Hardware.Communication
{
    using System;
    using System.Linq;

    using Unosquare.RaspberryIO.Gpio;
    using Unosquare.RaspberryIO.Native;

    public class PiGpio : HardwareProtocol<GpioChannel>
    {
        private static GpioController gpio = GpioController.Instance;

        public override byte ReadByte(GpioChannel channel)
        {
            ValidateChannel(channel);
            int pin = toPinId(channel.Pin);
            int result;
            switch (channel.Mode)
            {
                case GpioMode.Analog:
                    result = WiringPi.AnalogRead(pin);
                    break;
                case GpioMode.Digital:
                    result = WiringPi.DigitalRead(pin);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid pin operation");
                    break;
            }

            return (byte)(result & 0xFF);
        }

        public override int Write(GpioChannel channel, byte data)
        {
            ValidateChannel(channel);
            int pin = toPinId(channel.Pin);
            GpioPin gpioPin = toGpioPin(channel.Pin);

            switch (channel.Mode)
            {
                case GpioMode.Analog:
                    gpioPin.PinMode = GpioPinDriveMode.Output;
                    WiringPi.AnalogWrite(pin, (int)data);
                    break;
                case GpioMode.Digital:
                    gpioPin.PinMode = GpioPinDriveMode.Output;
                    WiringPi.DigitalWrite(pin, data);
                    break;
                case GpioMode.Pwm:
                    gpioPin.PinMode = GpioPinDriveMode.PwmOutput;
                    if (gpioPin.IsInSoftPwmMode)
                    {
                        gpioPin.StartSoftPwm(data, 255);
                    }
                    else
                    {
                        WiringPi.PwmWrite(pin, data);
                    }

                    break;
                default:
                    throw new InvalidOperationException($"Invalid pin operation");
                    break;
            }

            return 1;
        }

        protected override void ValidateChannel(GpioChannel channel)
        {
            if (channel.Mode == GpioMode.Undefined)
            {
                throw new ArgumentOutOfRangeException("Channel mode invalid");
            }

            if (channel.Pin == WiringPiPin.Unknown)
            {
                throw new ArgumentOutOfRangeException("Channel pin invalid");
            }

            int pin = toPinId(channel.Pin);

            if (channel.Mode == GpioMode.Pwm && !gpio.Pins[pin].Capabilities.Contains(PinCapability.PWM))
            {
                throw new InvalidOperationException("Channel not PWM capable");
            }
            else if (!gpio.Pin00.Capabilities.Contains(PinCapability.GP))
            {
                throw new InvalidOperationException("Channel not GPIO capable");
            }
        }

        private GpioPin toGpioPin(WiringPiPin pin) => gpio.Pins[toPinId(pin)];

        private int toPinId(WiringPiPin pin) => WiringPi.WpiPinToGpio((int)pin);
    }
}