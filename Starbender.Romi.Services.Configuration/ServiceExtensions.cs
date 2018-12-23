using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Services.Configuration
{
    using System.IO;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using NLog;
    using NLog.Config;
    using NLog.Extensions.Logging;
    using NLog.Layouts;

    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;

    using LogLevel = Microsoft.Extensions.Logging.LogLevel;

    public static class ServiceExtensions
    {
        public static void AddRomi(this IServiceCollection services,IConfiguration configuration)
        {
            var settings = new RomiSettings();

            configuration.Bind("RomiSettings", settings);

            ConfigurePaths(settings);
            ConfigureNLog(settings);

            services.AddSingleton<IRomiSettings>(settings);

            ConfigureNLog(settings);

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
                            .AddConsole()
                            .AddDebug();
                    });

            services.AddDbContext<RomiDbContext>(
                options => options.UseSqlite(
                    $"DataSource={settings.DataPath}/romi.db",
                    builder => builder.MigrationsAssembly(typeof(RomiDbContext).Assembly.FullName)));
        }

        public static void UseRomi(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<RomiDbContext>();
                context.Database.EnsureCreated();
            }
        }

        private static void ConfigurePaths(RomiSettings settings)
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

        private static void ConfigureNLog(RomiSettings settings)
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
