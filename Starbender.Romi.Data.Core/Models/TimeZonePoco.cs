namespace Starbender.Romi.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TimeZonePoco
    {
        [NotMapped]
        public ICollection<TimeZoneAliasPoco> Aliases { get; set; }

        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        public int Id { get; set; }

        public ICollection<TimeZoneIntervalPoco> Intervals { get; set; }

        public string Name { get; set; }

        public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;
    }
}