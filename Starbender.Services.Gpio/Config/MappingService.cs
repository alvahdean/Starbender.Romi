namespace Starbender.Romi.Services.Configuration
{
    using System;
    using System.Linq;

    using AutoMapper;

    using NodaTime;

    using Starbender.Core.Automapper;
    using Starbender.Services.Gpio.WiringPi.Models;

    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.WiringPi;

    public class MappingService : IMappingDefinitionService
    {
        public void InitalizeMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<IGpioPin, GpioPinModel>();
            config.CreateMap<IGpioPinModel,GpioPin>();
        }
    }
}