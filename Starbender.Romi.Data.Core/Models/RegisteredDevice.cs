namespace Starbender.Romi.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class RegisteredDevice
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The application host that manages this device
        /// </summary>
        public RomiApplicationHost ApplicationHost { get; set; }

        /// <summary>
        /// The device interface that this device implements
        /// </summary>
        public RegisteredInterface Interface { get; set; }

        /// <summary>
        /// Device Name
        /// </summary>
        public string Name { get; set; }
    }
}
