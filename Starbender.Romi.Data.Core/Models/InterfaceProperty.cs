namespace Starbender.Romi.Data.Models
{
    public class InterfaceProperty
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary> 
        /// Gets or sets the interface that implements this property 
        /// </summary>
        public RegisteredInterface Interface { get; set; }

        /// <summary> 
        /// Gets or sets the property name 
        /// </summary>
        public string Name { get; set; }
    }
}