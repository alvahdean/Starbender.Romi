﻿namespace Starbender.Romi.Data.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Configuration object containing the device interfaces and
    /// implemented devices that are supported in the web service
    /// </summary>
    public class RomiApplicationHost
    {
        /// <summary>
        /// Supported devices
        /// </summary>
        public List<RegisteredDevice> Devices { get; set; }

        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Registry Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Supported devices
        /// </summary>
        public List<SensorPoco> Sensors { get; set; }

        /// <summary>
        /// Settings for the Host
        /// </summary>
        public RomiSettings Settings { get; set; }
    }
}