using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi.Models.Gpio
{
    public enum EdgeDetectionEnum
    {
        /// <summary>Falling Edge</summary>
        FallingEdge,

        /// <summary>Rising edge</summary>
        RisingEdge,

        /// <summary>Both, falling and rising edges</summary>
        FallingAndRisingEdge,
    }
}
