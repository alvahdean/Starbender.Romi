namespace Starbender.Romi.Services.Configuration
{
    using System;
    using System.Linq;

    using AutoMapper;

    using NodaTime;

    using Starbender.Core.Automapper;
    using Starbender.Romi.Data.Models;

    public class MappingService : IMappingDefinitionService
    {
        public void InitalizeMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<HostSettings, RomiSettings>();
            config.CreateMap<RomiSettings, HostSettings>().ConstructUsing(t => new HostSettings(t));

            // todo: Fix this!
            config.CreateMap<TimeZonePoco, DateTimeZone>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MinOffset, opt => opt.MapFrom(src => src.Intervals.First().OffsetMinutes))
                .ForMember(
                    dest => dest.MaxOffset,
                    opt => opt.MapFrom(
                        src => src.Intervals.First().OffsetMinutes
                               + (src.Intervals.First().UtcEnd - src.Intervals.First().UtcStart).TotalMinutes));
            config.CreateMap<IDateTimeZoneProvider, TimeZoneVersionPoco>().ConstructUsing(
                t => new TimeZoneVersionPoco() { Loaded = DateTimeOffset.UtcNow, Version = t.VersionId });
        }
    }
}