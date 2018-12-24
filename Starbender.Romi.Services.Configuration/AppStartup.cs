using System;
using System.Collections.Generic;
using System.Text;
using Nito.AsyncEx;
using Nito.AsyncEx.Synchronous;

namespace Starbender.Romi.Services.Configuration
{
    using System.IO;
    using System.Linq;
    using System.Net;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using NLog;
    using NLog.Common;
    using NLog.Config;
    using NLog.Extensions.Logging;
    using NLog.Layouts;

    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;

    using LogLevel = Microsoft.Extensions.Logging.LogLevel;

    public static class AppStartup
    {
        private static string _connectionString = "";
        public static IServiceCollection AddRomi(this IServiceCollection services, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Romi") ?? "Data Source=/apps/romi/data/romi.db";

            var dbOptions = new DbContextOptionsBuilder<RomiDbContext>().UseSqlite(
                _connectionString,
                builder => builder.MigrationsAssembly(typeof(RomiDbContext).Assembly.FullName));

            var dbContext = new RomiDbContext(dbOptions.Options);

            services.AddScoped(_ => dbContext);

            services.AddSingleton<IRomiSettings>(RomiSettings.GetDefault());

            services.AddLogging(
                builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Trace);
                        builder.AddNLog(
                                new NLogProviderOptions
                                    {
                                        CaptureMessageTemplates = true,
                                        CaptureMessageProperties = true
                                    })
                            .AddConsole().AddDebug();
                    });

            return services;
        }

        public static IApplicationBuilder UseRomi(this IApplicationBuilder app)
        {
            var dbContext = app.ApplicationServices.GetService<RomiDbContext>();

            dbContext.Database.Migrate();

            var appHost = dbContext.ApplicationHosts.Find(new object[] { 1 });

            if (appHost == null)
            {
                appHost = new RomiApplicationHost() { Id = 1, Name = "." };
            }

            if (appHost.Settings == null)
            {
                appHost.Settings = RomiSettings.GetDefault();
            }

            dbContext.SaveChanges();

            var cfg = appHost.Settings;
            ConfigurePaths(cfg);
            ConfigureNLog(cfg);

            var svcCfg = app.ApplicationServices.GetService<IRomiSettings>();
            svcCfg.ApiRoot = cfg.ApiRoot;
            svcCfg.ApplicationPath = cfg.ApplicationPath;
            svcCfg.LogPath = cfg.LogPath;
            svcCfg.DataPath = cfg.DataPath;
            svcCfg.ServiceHost = cfg.ServiceHost;
            svcCfg.ApiVersion = cfg.ApiVersion;
            svcCfg.ServicePort = cfg.ServicePort;
            return app;
        }

        private static void ConfigurePaths(Data.Models.RomiSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.ApplicationPath))
                settings.ApplicationPath = "/apps/romi";

            Directory.CreateDirectory(settings.ApplicationPath);

            if (string.IsNullOrWhiteSpace(settings.LogPath))
            {
                settings.LogPath = "logs";
            }

            if (!Path.IsPathRooted(settings.LogPath))
            {
                settings.LogPath = $"{settings.ApplicationPath}/{settings.LogPath}";
            }

            Directory.CreateDirectory(settings.LogPath);

            if (string.IsNullOrWhiteSpace(settings.DataPath))
            {
                settings.DataPath = "data";
            }

            if (!Path.IsPathRooted(settings.DataPath))
            {
                settings.DataPath = $"{settings.ApplicationPath}/{settings.DataPath}";
            }

            Directory.CreateDirectory(settings.DataPath);

        }

        private static void ConfigureNLog(Data.Models.RomiSettings settings)
        {
            var logConfig = new XmlLoggingConfiguration("NLog.config");

            SimpleLayout logDirectory = null;
            if (!logConfig.Variables.TryGetValue("logDirectory", out logDirectory))
            {
                logConfig.Variables.Add("logDirectory", new SimpleLayout(settings.LogPath));
            }
            else
            {
                logConfig.Variables["logDirectory"] = settings.LogPath;
            }
            LogManager.Configuration = logConfig;
        }
    }
}
