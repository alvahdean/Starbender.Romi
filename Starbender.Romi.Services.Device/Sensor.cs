using System;

namespace Starbender.Romi.Services.Device
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Starbender.Romi.Data.Models;

    public class Sensor : SensorInfo
    {
        protected readonly List<string> _supportedTypes = new List<string>();

        protected readonly Dictionary<string,List<string>> _supportedNames = new Dictionary<string, List<string>>();

        private readonly IMapper _mapper;

        internal Sensor(IMapper mapper)
        {
            TypeName = this.GetType().FullName;
            this._mapper = mapper;
        }

        internal Sensor(SensorInfo info, IMapper mapper) : this(mapper)
        {
            if (info == null)
            {
                throw new ArgumentNullException();
            }

            var typeName = this.GetType().FullName;

            if (info.TypeName != typeName)
            {
                throw new Exception("Type does not match target sensor");
            }

            Id = info.Id;
            TypeName = info.TypeName;
        }

        public virtual bool Supports(string sensorType, string sensorName)
        {
            return this._supportedTypes.Contains(sensorType) && this._supportedNames[sensorType].Contains(sensorName);
        }

        public IEnumerable<string> SupportedTypes => this._supportedTypes;

        public IEnumerable<string> SupportedNames(string sensorType) => this._supportedNames[sensorType];

        protected void EnsureSupported(string sensorType, string sensorName)
        {
            if (!Supports(sensorType, sensorName))
            {
                throw new NotSupportedException($"Sensor does not support {sensorType}:{sensorName}");
            }
        }

        public virtual SensorResult Read(string sensorType, string sensorName)
        {
            var result = new SensorResult() { Sensor = this._mapper.Map<SensorPoco>(this) };

            return result;
        }

        public virtual async Task<SensorResult> ReadAsync(string sensorType, string sensorName)
        {
            return await Task.Run(() => Read(sensorName, sensorType));
        }
    }
}
