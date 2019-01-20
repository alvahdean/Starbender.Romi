namespace Starbender.Romi.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TimeZoneVersionPoco
    {
        public int Id { get; set; }

        public DateTimeOffset Loaded { get; set; }

        [NotMapped]
        public string ShortVersion => Version?.Split(' ')[1];

        public string Version { get; set; }
    }
}