namespace Starbender.Romi.WebApi.Config
{
    using AutoMapper;

    using Starbender.Core.Automapper;
    using Starbender.Romi.WebApi.Models;
    using Starbender.Services.RaspberryPi.Models.Gpio;

    public class MappingService : IMappingDefinitionService
    {
        public void InitalizeMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<UpdateGpioPinRequest, GpioPinModel>().ReverseMap();
            config.CreateMap<GpioPinResponse, GpioPinModel>().ReverseMap();
        }
    }
}