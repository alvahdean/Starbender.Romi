using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Services.Configuration
{
    using Starbender.Romi.Data.Models;

    public class RomiSettings : Data.Models.RomiSettings
    {
        public static RomiSettings GetDefault()
        {
            string appPath = "/apps/romi";

            return new RomiSettings()
                       {
                           ServicePort = 1865,
                           ApiRoot = "/api",
                           ApiVersion = "0.1",
                           ApplicationPath = appPath,
                           LogPath = $"{appPath}/logs",
                           DataPath = $"{appPath}/data"
                       };
        }

        public string ConnectionString => $"Data Source={DataPath}/romi.db;";
    }
}
