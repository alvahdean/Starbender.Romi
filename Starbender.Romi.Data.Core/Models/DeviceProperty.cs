namespace Starbender.Romi.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class DeviceProperty
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The device interface that this property is associated with
        /// </summary>
        public RegisteredDevice Device { get; set; }

        /// <summary>
        /// Interface property
        /// </summary>
        public InterfaceProperty Property { get; set; }

        /// <summary>
        /// Property Value
        /// </summary>
        public string Value { get; set; }
    }
}
