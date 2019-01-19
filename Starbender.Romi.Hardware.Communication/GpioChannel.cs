using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Hardware.Communication
{
    using Unosquare.RaspberryIO.Gpio;
    using Unosquare.RaspberryIO.Native;

    public class GpioChannel
    {
        public WiringPiPin Pin { get; set; } = WiringPiPin.Unknown;

        public GpioMode Mode { get; set; } = GpioMode.Undefined;
    }
}
