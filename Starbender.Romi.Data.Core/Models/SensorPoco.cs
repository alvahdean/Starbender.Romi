using System;

namespace Starbender.Romi.Data.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SensorPoco
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        public RomiApplicationHost Host { get; set; }
    }
}
