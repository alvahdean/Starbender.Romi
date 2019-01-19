namespace Starbender.Romi.Services.Device
{
    using Starbender.Romi.Data.Models;

    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;

    using System.Linq;
    using System.Text;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using Starbender.Core.Data;
    using Starbender.Romi.Data;

    public class DeviceService : IDeviceService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DeviceService(ILogger<DeviceService> logger, IMapper mapper)
        {
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<TSensor> Register<TSensor>()
            where TSensor : Sensor, new()
        {
            Type sensorType = typeof(TSensor);
            string typeName = sensorType.FullName;
            TSensor result;
            this._logger.LogDebug($"Registering Sensor: {typeName}");
            bool isRegistered = await IsRegisteredType(typeName);
            if (!isRegistered)
            {
                using (var uow = new UnitOfWork<SensorPoco>((DbContext)new RomiDbContext()))
                {
                    SensorPoco poco = new SensorPoco() { TypeName = typeName };
                    uow.Repository().Add(poco);
                }
            }
            else
            {
                this._logger.LogWarning($"Sensor is already registered: {typeName}");
            }
            result= await this.GetSensor<TSensor>();
            return result;
        }

        public async Task<Sensor> Register(string typeName)
        {
            bool isRegistered = await IsRegisteredType(typeName);
            if (!isRegistered)
            {
                using (var uow = new UnitOfWork<SensorPoco>(new RomiDbContext()))
                {
                    SensorPoco poco = new SensorPoco() { TypeName = typeName };
                    uow.Repository().Add(poco);
                }
            }

            Sensor result = await this.GetSensor(typeName);
            return result;
        }

        public async Task<bool> IsRegisteredType(string sensorType)
        {
            bool result = false;
            using (var uow = new UnitOfWork<SensorPoco>(new RomiDbContext()))
            {
                var poco = await uow.Repository().SingleOrDefaultAsync(x => x.TypeName == sensorType);
                result = poco != null;
            }

            return result;
        }

        public async Task<bool> IsRegisteredType(Type sensorType) => await IsRegisteredType(sensorType.FullName);

        public async Task<bool> IsRegisteredType<TSensor>() => await IsRegisteredType(typeof(TSensor));

        public async Task<IEnumerable<SensorInfo>> GetSensors()
        {
            IEnumerable<SensorInfo> result = new List<SensorInfo>();

            using (var uow = new UnitOfWork<SensorPoco>(new RomiDbContext()))
            {
                var sensorList = await uow.Repository().AllAsync();
                result = _mapper.Map<IEnumerable<SensorInfo>>(sensorList);
            }

            return result;
        }

        public async Task<TSensor> GetSensor<TSensor>()
            where TSensor : Sensor, new()
        {
            Type sensorType = typeof(TSensor);
            string typeName = sensorType.FullName;
            var info = await GetSensorInfo(typeName);
            var result = new TSensor() { Id = info.Id, TypeName = info.TypeName };
            return result;
        }

        public async Task<Sensor> GetSensor(SensorInfo sensorInfo)
        {
            return await GetSensor(sensorInfo?.TypeName);
        }

        public async Task<Sensor> GetSensor(int sensorId)
        {
            var info = await GetSensorInfo(sensorId);
            return await GetSensor(info);
        }

        public async Task<Sensor> GetSensor(string typeName)
        {
            Sensor result = null;

            using (var uow = new UnitOfWork<SensorPoco>(new RomiDbContext()))
            {
                var poco = await GetSensorInfo(typeName);
                if (poco != null)
                {
                    Type sensorType = Type.GetType(poco.TypeName, true, true);
                    result = (Sensor)Activator.CreateInstance(sensorType, true);
                    result.TypeName = poco.TypeName;
                    result.Id = poco.Id;
                    return result;
                }
            }

            return result;
        }

        public void UpdateSensor(SensorInfo sensor)
        {
            this._logger.LogInformation($"Updating sensor: {sensor.TypeName}");
            using (var uow = new UnitOfWork<SensorPoco>(new RomiDbContext()))
            {
                var poco = this._mapper.Map<SensorPoco>(sensor);
                uow.Repository().Update(poco);
            }
        }

        public void DeleteSensor(SensorInfo sensor)
        {
            this._logger.LogInformation($"Deleting sensor: {sensor.TypeName}");
            using (var uow = new UnitOfWork<SensorPoco>(new RomiDbContext()))
            {
                var poco = this._mapper.Map<SensorPoco>(sensor);
                uow.Repository().Delete(poco);
            }
        }

        private async Task<SensorInfo> GetSensorInfo(string typeName)
        {
            Type sensorType = Type.GetType(typeName, true, true);
            SensorInfo result;

            using (var uow = new UnitOfWork<SensorPoco>(new RomiDbContext()))
            {
                var poco = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<SensorPoco>(uow.Repository().Query(x => x.TypeName == typeName).Include(t=>t.Host));
                result = this._mapper.Map<SensorInfo>(poco);
            }

            return result;
        }

        private async Task<SensorInfo> GetSensorInfo(int sensorId)
        {
            SensorInfo result;

            using (var uow = new UnitOfWork<SensorPoco>(new RomiDbContext()))
            {
                var poco = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync<SensorPoco>(uow.Repository().Query(x => x.Id == sensorId).Include(t=>t.Host));
                result = this._mapper.Map<SensorInfo>(poco);
            }

            return result;
        }
    }
}
