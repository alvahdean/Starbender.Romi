namespace Starbender.Romi.Services.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using NLog;
    using NLog.Config;
    using NLog.Extensions.Logging;
    using NLog.Layouts;

    using LogLevel = Microsoft.Extensions.Logging.LogLevel;

    /// <summary>
    /// Central dependency injection configuration for ROMI
    /// </summary>
    public static class DependencyInjection
    {
        public static RomiSettings LoadSettings(IConfiguration configuration=null)
        {
            if (configuration == null)
            {
                configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            }
            var settings = new RomiSettings();

            configuration.Bind("RomiSettings", settings);

            ConfigurePaths(settings);

            return settings;
        }

        /// <summary>
        /// Configuration entry point for the ROMI application services
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="services"></param>
        public static void Configure(IConfiguration configuration, IServiceCollection services)
        {
            var settings = LoadSettings(configuration);

            services.AddSingleton(settings);

            ConfigureNLog(settings);

            services.AddLogging(
                builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Trace);
                        builder.AddNLog(
                                new NLogProviderOptions
                                    {
                                        CaptureMessageTemplates = true, CaptureMessageProperties = true
                                    })
                            .AddConsole()
                            .AddDebug();
                    });

            services.Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });
        }

        public static void ConfigureData(RomiSettings settings)
        {

        }

        public static void ConfigurePaths(RomiSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.ApplicationPath))
                settings.ApplicationPath = "C:/app/romi";

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

        public static void ConfigureNLog(RomiSettings settings)
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
