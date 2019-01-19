using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Hardware.Communication
{
    using Unosquare.RaspberryIO.Gpio;
    using Unosquare.RaspberryIO.Native;

    public enum GpioMode
    {
        Undefined,
        Analog,
        Digital,
        Pwm,
    }
}
