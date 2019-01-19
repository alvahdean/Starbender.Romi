using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Data.Models
{
    public class TimeZoneIntervalPoco
    {
        public int Id { get; set; }
        public DateTime UtcStart { get; set; }
        public DateTime UtcEnd { get; set; }
        public DateTime LocalStart { get; set; }
        public DateTime LocalEnd { get; set; }
        public int OffsetMinutes { get; set; }
        public string Abbreviation { get; set; }
        public TimeZonePoco TimeZone { get; set; }
    }
}
