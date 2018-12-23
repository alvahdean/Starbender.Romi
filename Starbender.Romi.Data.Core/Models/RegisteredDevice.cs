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
        /// Device Name
        /// </summary>
        public string Name { get; set; }
    }
}
