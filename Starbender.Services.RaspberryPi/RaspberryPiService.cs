namespace Starbender.Services.RaspberryPi
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using Starbender.Contracts;
    using Starbender.Services.RaspberryPi.Models;

    using Unosquare.RaspberryIO;
    using Unosquare.RaspberryIO.Gpio;
    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.RaspberryIO.Camera;
    using Unosquare.RaspberryIO.Computer;
    using Unosquare.Swan.Components;


    public class RaspberryPiService : IRaspberryPiService
    {
        private readonly IMapper _mapper;

        private readonly IPiGpioService _gpioService;

        private readonly IPiSpiService _spiService;

        private readonly IPiI2CService _i2cService;

        private readonly IPiCameraService _cameraService;

        private readonly IPiDisplayService _displayService;

        public RaspberryPiService(
            IMapper mapper,
            IPiSpiService spiService,
            IPiGpioService gpioService,
            IPiI2CService i2cService,
            IPiCameraService cameraService,
            IPiDisplayService displayService)
        {
            _mapper = mapper;
            _spiService = spiService;
            _gpioService = gpioService;
            _i2cService = i2cService;
            _cameraService = cameraService;
            _displayService = displayService;
        }

        /// <summary>
        /// Provides information on this Raspberry Pi's CPU and form factor.
        /// </summary>
        public SystemInfoModel Info => _mapper.Map<SystemInfoModel>(Pi.Info);

        /// <summary>
        /// Provides access to the Raspberry Pi's GPIO as a collection of GPIO Pins.
        /// </summary>
        public IPiGpioService GpioService => _gpioService;

        /// <summary>
        /// Provides access to the 2-channel SPI bus.
        /// </summary>
        public IPiSpiService SpiService => _spiService;

        /// <summary>
        /// Provides access to the functionality of the i2c bus.
        /// </summary>
        public IPiI2CService I2CService => _i2cService;

        /// <summary>
        /// Provides access to the official Raspberry Pi Camera.
        /// </summary>
        public IPiCameraService CameraService => _cameraService;

        /// <summary>
        /// Provides access to the official Raspberry Pi 7-inch DSI Display.
        /// </summary>
        public IPiDisplayService DisplayService => _displayService;

        /// <summary>
        /// Restarts the Pi. Must be running as SU.
        /// </summary>
        /// <returns>The process result.</returns>
        public async Task<ProcessResultModel> Restart()
        {
            var piResult = await ProcessRunner.GetProcessResultAsync("reboot", null, null);
            return this._mapper.Map<ProcessResultModel>(piResult);
        }

        /// <summary>
        /// Halts the Pi. Must be running as SU.
        /// </summary>
        /// <returns>The process result.</returns>
        public async Task<ProcessResultModel> Shutdown()
        {
            var piResult= await ProcessRunner.GetProcessResultAsync("halt", null, null);
            return this._mapper.Map<ProcessResultModel>(piResult);
        }
    }
}