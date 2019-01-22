namespace Starbender.Romi.Services.Device
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDeviceService
    {
        void DeleteSensor(SensorInfo sensor);

        Task<Sensor> GetSensor(SensorInfo sensorInfo);

        Task<Sensor> GetSensor(string typeName);

        Task<Sensor> GetSensor(int sensorId);

        Task<TSensor> GetSensor<TSensor>()
            where TSensor : Sensor, new();

        Task<IEnumerable<SensorInfo>> GetSensors();

        Task<bool> IsRegisteredType(string sensorType);

        Task<TSensor> Register<TSensor>()
            where TSensor : Sensor, new();

        Task<Sensor> Register(string typeName);

        void UpdateSensor(SensorInfo sensor);
    }
}