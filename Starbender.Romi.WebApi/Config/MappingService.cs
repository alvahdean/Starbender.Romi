namespace Starbender.Romi.Web.Service.Config
{
    using AutoMapper;

    using Starbender.Core.Automapper;
    using Starbender.Romi.Web.Service.Models;
    using Starbender.Services.Gpio.WiringPi.Models;

    public class MappingService : IMappingDefinitionService
    {
        public void InitalizeMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<UpdateGpioPinRequest, GpioPinModel>().ReverseMap();
            config.CreateMap<GpioPinResponse, GpioPinModel>().ReverseMap();
        }
    }
}