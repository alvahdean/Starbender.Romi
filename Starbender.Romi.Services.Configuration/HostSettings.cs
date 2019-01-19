using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Services.Configuration
{
    using System.IO;

    using Starbender.Romi.Data.Models;

    public class HostSettings 
    {
        public static HostSettings Default
        {
            get
            {

                return new HostSettings()
                           {

                           };
            }
        }

        public HostSettings()
        {
            string appPath = "/apps/romi";
            ServicePort = 1865;
            ApiRoot = "/api";
            ApiVersion = "0.1";
            ApplicationPath = appPath;
            LogPath = $"{appPath}/logs";
            DataPath = $"{appPath}/data";
        }

        public HostSettings(RomiSettings settings) : this()
        {
            if (settings != null)
            {
                ServicePort = settings.ServicePort>0 ? settings.ServicePort : 1865;
                ApiRoot = settings.ApiRoot ?? ApiRoot;
                ApiVersion = settings.ApiVersion ?? ApiVersion;
                ApplicationPath = settings.ApplicationPath ?? ApplicationPath;
                LogPath = settings.LogPath ?? LogPath;
                DataPath = settings.DataPath ?? DataPath;
            }

            if (!Path.IsPathRooted(LogPath))
            {
                LogPath = $"{ApplicationPath}/{LogPath}";
            }

            if (!Path.IsPathRooted(DataPath))
            {
                DataPath = $"{ApplicationPath}/{DataPath}";
            }
        }

        public string ConnectionString => $"Data Source={DataPath}/romi.db;";

        /// <summary>
        /// Primary Key
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// Host part of the service Url
        /// </summary>
        public string ServiceHost { get; internal set; }

        /// <summary>
        /// The port that the service will listen
        /// </summary>
        public int ServicePort { get; internal set; }

        /// <summary>
        /// The root path to the REST API
        /// </summary>
        public string ApiRoot { get; internal set; }

        /// <summary>
        /// The API version number (appended to the ApiRoot path)
        /// </summary>
        public string ApiVersion { get; internal set; }

        /// <summary>
        /// The local directory where the application will run from
        /// </summary>
        public string ApplicationPath { get; internal set; }

        /// <summary>
        /// The directory to which logs will be written
        /// </summary>
        public string LogPath { get; internal set; }

        /// <summary>
        /// The directory where application data is stored
        /// </summary>
        public string DataPath { get; internal set; }

    }
}
