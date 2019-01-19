namespace Starbender.Romi.Web.Service.Module
{
    using System;
    using System.Collections.Generic;

    using Autofac;

    using Starbender.Core.Automapper;
    using Starbender.Core.Data;

    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterAutoMapper(builder);
        }

        private void RegisterAutoMapper(ContainerBuilder builder)
        {
            builder.RegisterType<AutoMapperService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<DateMappingDefinitionService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<OptionalMappingDefinitionService>().AsImplementedInterfaces().SingleInstance();
        }
    }
}