using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Module = Autofac.Module;

namespace Starbender.Romi.Services.Configuration
{
    using AutoMapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using NLog;
    using NLog.Config;
    using NLog.Extensions.Logging;
    using NLog.Layouts;

    using NodaTime;

    using Starbender.Contracts;
    using Starbender.Core.Time;
    using Starbender.Romi.Data;
    using Starbender.Romi.Services.Device;

    using LogLevel = Microsoft.Extensions.Logging.LogLevel;

    public class DependencyInjection : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.IsClass && !x.IsAbstract && (x.Name.EndsWith("Provider") || x.Name.EndsWith("Service") || x.Name.EndsWith("Factory")))
                .AsImplementedInterfaces()
                .SingleInstance();
            IDateTimeZoneProvider tzProvider = CurrentTzdbProvider.Load();
            builder.RegisterInstance<IDateTimeZoneProvider>(tzProvider);
        }
    }
}
