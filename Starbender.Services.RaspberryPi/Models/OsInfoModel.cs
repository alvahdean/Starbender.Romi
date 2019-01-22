using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi.Models
{
    public class OsInfoModel
    {
        /// <summary>System name.</summary>
        public string SysName { get; set; }

        /// <summary>Node name.</summary>
        public string NodeName { get; set; }

        /// <summary>Release level.</summary>
        public string Release { get; set; }

        /// <summary>Version level.</summary>
        public string Version { get; set; }

        /// <summary>Hardware level.</summary>
        public string Machine { get; set; }

        /// <summary>Domain name.</summary>
        public string DomainName { get; set; }
    }
}
