using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi.Models.Gpio
{
    /// <summary>Defines the different pin capabilities.</summary>
    public enum PinCapabilityEnum
    {
        /// <summary>
        /// General Purpose capability: Digital and Analog Read/Write
        /// </summary>
        GP,

        /// <summary>General Purpose Clock (not PWM)</summary>
        GPCLK,

        /// <summary>i2c data channel</summary>
        I2CSDA,

        /// <summary>i2c clock channel</summary>
        I2CSCL,

        /// <summary>SPI Master Out, Slave In channel</summary>
        SPIMOSI,

        /// <summary>SPI Master In, Slave Out channel</summary>
        SPIMISO,

        /// <summary>SPI Clock channel</summary>
        SPICLK,

        /// <summary>SPI Chip Select Channel</summary>
        SPICS,

        /// <summary>UART Request to Send Channel</summary>
        UARTRTS,

        /// <summary>UART Transmit Channel</summary>
        UARTTXD,

        /// <summary>UART Receive Channel</summary>
        UARTRXD,

        /// <summary>Hardware Pule Width Modulation</summary>
        PWM,
    }
}
