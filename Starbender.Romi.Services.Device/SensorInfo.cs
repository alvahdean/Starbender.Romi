using System;

namespace Starbender.Romi.Services.Device
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Starbender.Romi.Data.Models;

    public class SensorInfo
    {
        //public static implicit operator SensorInfo(SensorPoco poco)
        //{
        //    return new SensorInfo() { Id = poco.Id, TypeName = poco.TypeName, };
        //}

        //public static implicit operator SensorPoco(SensorInfo info)
        //{
        //    return new SensorPoco() { Id = info.Id, TypeName = info.TypeName };
        //}

        public int Id { get; set; }

        public string TypeName { get; set; }

        public RomiApplicationHost Host { get; set; }

    }
}
