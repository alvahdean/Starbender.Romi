using System;

namespace Starbender.Romi.Services.Device
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using AutoMapper;

    using NodaTime;

    using Starbender.Romi.Data.Models;

    public class TimeSensor : Sensor
    {
        private readonly IMapper _mapper;

        public TimeSensor(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
            DefaultTimeZone = DateTimeZone.Utc;
            this._supportedTypes.AddRange(new[] { "Time" });
            //this._supportedNames.Add("Time", TimeZoneInfo.GetSystemTimeZones().Select(t => t.StandardName).ToList());
            this._supportedNames.Add("Time", NodaTime.DateTimeZoneProviders.Tzdb.Ids.ToList());
        }

        public DateTimeZone DefaultTimeZone { get; set; }

        public override SensorResult Read(string sensorType, string sensorName)
        {
            sensorType = string.IsNullOrWhiteSpace(sensorType) ? "Time" : sensorType;

            sensorName = string.IsNullOrWhiteSpace(sensorName) ? DefaultTimeZone.Id : sensorName;

            SensorResult result = base.Read(sensorType, sensorName);
            try
            {
                EnsureSupported(sensorType, sensorName);
                
                var tz = NodaTime.DateTimeZoneProviders.Tzdb.GetZoneOrNull(sensorName);
                result.Attribute = tz.Id;
                result.Result = SystemClock.Instance.GetCurrentInstant().InZone(tz);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public override async Task<SensorResult> ReadAsync(string sensorType, string sensorName)
        {
            return await Task.Run(() => Read(sensorType, sensorName));
        }
    }
}