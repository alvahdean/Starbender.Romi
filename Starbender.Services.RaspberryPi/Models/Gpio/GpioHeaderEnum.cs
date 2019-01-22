using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi.Models.Gpio
{
    public enum GpioHeaderEnum
    {
        /// <summary>Not defined</summary>
        None,
        /// <summary>P1 connector (main connector)</summary>
        P1,
        /// <summary>P5 connector (auxiliary, not commonly used)</summary>
        P5,
    }
}
