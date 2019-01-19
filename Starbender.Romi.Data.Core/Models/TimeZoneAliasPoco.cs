using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Data.Models
{
    public class TimeZoneAliasPoco
    {
        public int TimeZoneId { get; set; }
        public TimeZonePoco TimeZone { get; set; }

        public int? CanonicalId { get; set; }
        public TimeZonePoco CanonicalZone { get; set; }
    }
}
