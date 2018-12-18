using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Services.Configuration
{
    using Starbender.Romi.Data.Models;

    public class RomiSettings : Data.Models.RomiSettings
    {
        public string ConnectionString => $"Data Source={DataPath}/romi.db;";
    }
}
