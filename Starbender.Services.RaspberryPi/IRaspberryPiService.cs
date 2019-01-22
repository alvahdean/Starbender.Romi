namespace Starbender.Services.RaspberryPi
{
    using System;
    using System.Threading.Tasks;

    using Starbender.Services.RaspberryPi.Models;
    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.WiringPi;

    public interface IRaspberryPiService
    {
        /// <summary>
        /// Provides information on this Raspberry Pi's CPU and form factor.
        /// </summary>
        SystemInfoModel Info { get; }

        /// <summary>
        /// Provides access to the Raspberry Pi's GPIO as a collection of GPIO Pins.
        /// </summary>
        IPiGpioService GpioService { get; }

        /// <summary>
        /// Provides access to the 2-channel SPI bus.
        /// </summary>
        IPiSpiService SpiService { get; }

        /// <summary>
        /// Provides access to the functionality of the i2c bus.
        /// </summary>
        IPiI2CService I2CService { get; }

        /// <summary>
        /// Provides access to the official Raspberry Pi Camera.
        /// </summary>
        IPiCameraService CameraService { get; }

        /// <summary>
        /// Provides access to the official Raspberry Pi 7-inch DSI Display.
        /// </summary>
        IPiDisplayService DisplayService { get; }

        /// <summary>
        /// Restarts the Pi. Must be running as SU.
        /// </summary>
        /// <returns>The process result.</returns>
        Task<ProcessResultModel> Restart();

        /// <summary>
        /// Halts the Pi. Must be running as SU.
        /// </summary>
        /// <returns>The process result.</returns>
        Task<ProcessResultModel> Shutdown();
    }
}