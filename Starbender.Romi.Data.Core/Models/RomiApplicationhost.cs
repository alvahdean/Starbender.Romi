namespace Starbender.Romi.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Configuration object containing the device interfaces and
    /// implemented devices that are supported in the web service
    /// </summary>
    public class RomiApplicationHost
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Registry Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Settings for the Host
        /// </summary>
        public RomiSettings Settings { get; set; }

        /// <summary>
        /// Supported devices
        /// </summary>
        public List<RegisteredDevice> Devices { get; set; }

        /// <summary>
        /// Supported devices
        /// </summary>
        public List<SensorPoco> Sensors { get; set; }

    }
}
