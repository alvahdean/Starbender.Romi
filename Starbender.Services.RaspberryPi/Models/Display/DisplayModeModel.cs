using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi.Models.Display
{
    public class DisplayModeModel
    {
        /// <summary>
        /// Gets a value indicating whether the Pi Foundation Display files are present.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is present; otherwise, <c>false</c>.
        /// </value>
        public bool IsPresent { get; }

        /// <summary>
        /// Gets or sets the brightness of the DSI display via filesystem.
        /// </summary>
        /// <value>The brightness.</value>
        public byte Brightness { get; set; }

        public double BrightnessPercent
        {
            get => Math.Round(Brightness / 255d,2);
            set => Brightness = (byte)((value - Math.Truncate(value)) * 255d);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the backlight of the DSI display on.
        /// This operation is performed via the file system.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is backlight on; otherwise, <c>false</c>.
        /// </value>
        public bool IsBacklightOn { get; set; }

        public string BacklightState => IsBacklightOn ? "ON" : "OFF";
    }
}
