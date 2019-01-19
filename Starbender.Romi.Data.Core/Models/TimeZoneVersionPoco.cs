using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class TimeZoneVersionPoco
    {
        public int Id { get; set; }
        
        public string Version { get; set; }

        public DateTimeOffset Loaded { get; set; }

        [NotMapped]
        public string ShortVersion => Version?.Split(' ')[1];

    }
}
