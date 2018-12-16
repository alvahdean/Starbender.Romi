namespace Starbender.Romi.Services.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Configuration object containing the device interfaces and
    /// implemented devices that are supported in the web service
    /// </summary>
    public class DeviceRegistry
    {
        /// <summary>
        /// Supported interfaces
        /// </summary>
        public List<SupportedInterface> SupportedInterfaces { get; set; }

        /// <summary>
        /// Supported devices
        /// </summary>
        public List<SupportedDevice> SupportedDevices { get; set; }
    }
}
