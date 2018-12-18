namespace Starbender.Romi.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class containing registration information for a device interface to be supported by the web service
    /// </summary>
    public class SupportedInterface
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Device interface name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Device interface version
        /// </summary>
        public int Version { get; set; }
    }
}
