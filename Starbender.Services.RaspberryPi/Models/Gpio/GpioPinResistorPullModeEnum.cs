using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi.Models.Gpio
{
    public enum GpioPinResistorPullModeEnum
    {
        /// <summary>Pull resistor not active. Line floating</summary>
        Off,
        /// <summary>
        /// Pull resistor sets a default value of 0 on no-connects
        /// </summary>
        PullDown,
        /// <summary>
        /// Pull resistor sets a default value of 1 on no-connects
        /// </summary>
        PullUp,
    }
}
