using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi.Models.Gpio
{
    public enum GpioPinDriveModeEnum
    {
        /// <summary>Input drive mode (perform reads)</summary>
        Input,

        /// <summary>Output drive mode (perform writes)</summary>
        Output,

        /// <summary>
        /// PWM output mode (only certain pins support this -- 2 of them at the moment)
        /// </summary>
        PwmOutput,

        /// <summary>
        /// GPIO Clock output mode (only a pin supports this at this time)
        /// </summary>
        GpioClock,

        /// <summary>The alt0 operating mode</summary>
        Alt0,

        /// <summary>The alt1 operating mode</summary>
        Alt1,

        /// <summary>The alt2 operating mode</summary>
        Alt2,

        /// <summary>The alt3 operating mode</summary>
        Alt3,
    }
}
