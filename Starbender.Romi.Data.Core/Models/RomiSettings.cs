using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Data.Models
{
    public class RomiSettings : IRomiSettings
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Host part of the service Url
        /// </summary>
        public string ServiceHost { get; set; }

        /// <summary>
        /// The port that the service will listen
        /// </summary>
        public int ServicePort { get; set; }

        /// <summary>
        /// The root path to the REST API
        /// </summary>
        public string ApiRoot { get; set; }

        /// <summary>
        /// The API version number (appended to the ApiRoot path)
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// The local directory where the application will run from
        /// </summary>
        public string ApplicationPath { get; set; }

        /// <summary>
        /// The directory to which logs will be written
        /// </summary>
        public string LogPath { get; set; }

        /// <summary>
        /// The directory where application data is stored
        /// </summary>
        public string DataPath { get; set; }

        /// <summary>
        /// Registry of devices and interfaces for the service
        /// </summary>
        public DeviceRegistry DeviceRegistry { get; set; }
    }
}
