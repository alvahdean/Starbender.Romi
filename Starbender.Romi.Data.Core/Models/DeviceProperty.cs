namespace Starbender.Romi.Data.Models
{
    public class DeviceProperty
    {
        /// <summary>
        /// The device interface that this property is associated with
        /// </summary>
        public RegisteredDevice Device { get; set; }

        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

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