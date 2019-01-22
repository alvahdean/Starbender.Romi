namespace Starbender.Services.RaspberryPi.Config
{
    using System;

    using AutoMapper;

    using Starbender.Core.Automapper;
    using Starbender.Services.RaspberryPi.Models;
    using Starbender.Services.RaspberryPi.Models.Display;
    using Starbender.Services.RaspberryPi.Models.Gpio;
    using Starbender.Services.RaspberryPi.Models.Spi;

    using Unosquare.RaspberryIO.Gpio;
    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.RaspberryIO.Computer;

    using SystemInfo = Unosquare.WiringPi.SystemInfo;

    public class MappingService : IMappingDefinitionService
    {
        public void InitalizeMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<IGpioPin, GpioPinModel>();
            config.CreateMap<IGpioPinModel, GpioPin>();
            config.CreateMap<SystemInfo, SystemInfoModel>();
            config.CreateMap<OsInfo, OsInfoModel>();
            config.CreateMap<SpiChannel, SpiChannelModel>();
            config.CreateMap<DsiDisplay, DisplayModeModel>().IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(t => t.BrightnessPercent, opt => opt.Ignore()).ReverseMap();
            config.CreateMap<PinCapability, PinCapabilityEnum>().ConvertUsing(
                (src) => (PinCapabilityEnum)Enum.Parse(typeof(PinCapabilityEnum), src.ToString()));
            config.CreateMap<PinCapabilityEnum, PinCapability>().ConvertUsing(
                (src) => (PinCapability)Enum.Parse(typeof(PinCapability), src.ToString()));
            config.CreateMap<BcmPin, BcmPinEnum>().ConvertUsing(
                (src) => (BcmPinEnum)Enum.Parse(typeof(BcmPinEnum), src.ToString()));
            config.CreateMap<BcmPinEnum, BcmPin>().ConvertUsing(
                (src) => (BcmPin)Enum.Parse(typeof(BcmPin), src.ToString()));
        }
    }
}