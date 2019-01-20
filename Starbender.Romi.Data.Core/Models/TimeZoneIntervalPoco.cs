namespace Starbender.Romi.Data.Models
{
    using System;

    public class TimeZoneIntervalPoco
    {
        public string Abbreviation { get; set; }

        public int Id { get; set; }

        public DateTime LocalEnd { get; set; }

        public DateTime LocalStart { get; set; }

        public int OffsetMinutes { get; set; }

        public TimeZonePoco TimeZone { get; set; }

        public DateTime UtcEnd { get; set; }

        public DateTime UtcStart { get; set; }
    }
}