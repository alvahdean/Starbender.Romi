using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi
{
    using AutoMapper;

    using Microsoft.Extensions.Logging;

    using Starbender.Services.RaspberryPi.Models.Display;

    using Unosquare.RaspberryIO;
    using Unosquare.RaspberryIO.Computer;

    public class PiDisplayService : IPiDisplayService
    {
        private readonly DsiDisplay _device;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public PiDisplayService(IMapper mapper, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PiDisplayService>();
            _device = Pi.PiDisplay;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a value indicating whether the Pi Foundation Display files are present.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is present; otherwise, <c>false</c>.
        /// </value>
        public bool IsPresent => this._device.IsPresent;

        /// <summary>
        /// Gets or sets the brightness of the DSI display as a percentage.
        /// </summary>
        /// <value>The brightness percentage</value>
        public double Brightness
        {
            get => this._device.Brightness / 255;
            set => this._device.Brightness = (byte)(value * 255);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the backlight of the DSI display on.
        /// This operation is performed via the file system.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is backlight on; otherwise, <c>false</c>.
        /// </value>
        public bool IsBacklightOn
        {
            get => this._device.IsBacklightOn;
            set => this._device.IsBacklightOn=value;
        }

        public DisplayModeModel Mode
        {
            get
            {
                var result = this._mapper.Map<DisplayModeModel>(this._device);

                this._logger.LogInformation($"Display Settings: Present = {result.IsPresent}, Backlight = {result.BacklightState}, Brightness = {result.BrightnessPercent*100}%");

                return result;
            }

            set
            {
                if (!this._device.IsPresent)
                {
                    this._logger.LogWarning($"UpdateSettings: Display not attached");
                    throw new NotSupportedException("Display not attached");
                }

                var newSettings = this._mapper.Map<DsiDisplay>(value);

                if (this._device.Brightness != newSettings.Brightness)
                {
                    this._logger.LogInformation($"Setting display brightness to {newSettings.Brightness} ({value.BrightnessPercent*100}%)");
                    this._device.Brightness = newSettings.Brightness;
                }

                if (this._device.IsBacklightOn != newSettings.IsBacklightOn)
                {
                    this._logger.LogInformation($"Setting backlight = {value.BacklightState}");
                    this._device.IsBacklightOn = newSettings.IsBacklightOn;
                }
            }
        }
    }
}
