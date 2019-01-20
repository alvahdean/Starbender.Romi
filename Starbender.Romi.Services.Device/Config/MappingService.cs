namespace Starbender.Romi.Services.Device
{
    using AutoMapper;

    using Starbender.Core.Automapper;
    using Starbender.Romi.Data.Models;

    public class MappingService : IMappingDefinitionService
    {
        public void InitalizeMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<SensorPoco, SensorInfo>().ReverseMap();
            config.CreateMap<Sensor, SensorInfo>().ReverseMap();
        }
    }
}