using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Data.Models
{
    public class TimeZonePoco
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }=DateTimeOffset.UtcNow;
        public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;
        public ICollection<TimeZoneIntervalPoco> Intervals { get; set; }
        public ICollection<TimeZoneAliasPoco> Aliases { get; set; }
    }
}
