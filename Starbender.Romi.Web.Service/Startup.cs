namespace Starbender.Romi.Web.Service
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;

    using Autofac;
    using Autofac.Integration.WebApi;

    using AutoMapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using NLog;
    using NLog.Config;
    using NLog.Extensions.Logging;
    using NLog.Layouts;

    using Owin;

    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;
    using Starbender.Romi.Services.Configuration;
    using Starbender.Romi.Services.Device;
    using Starbender.Romi.Web.Service.Module;

    using Swashbuckle.AspNetCore.Swagger;

    using LogLevel = Microsoft.Extensions.Logging.LogLevel;

    /// <summary>
    /// Web Service Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">The application configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">
        /// The services collection to be configured
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultIdentity<RomiUser>().AddEntityFrameworkStores<RomiDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "ROMI API", Version = "v1" }); });

            AddRomi(services, Configuration);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="env">The hosting environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            UseRomi(app);

            app.UseMvc( routes =>
                    {
                        routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}").MapRoute(
                            name: "apiDefault",
                            template: "api/{controller}/{action}/{id?}");
                    });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ROMI API V1");
                });
        }


        private void ConfigureWebApi(IAppBuilder app, ILifetimeScope container)
        {
            // configure WebApi using attribute based routing
            HttpConfiguration config = new HttpConfiguration();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            WebApiConfig.Register(config);

            app.UseWebApi(config);
            app.UseAutofacWebApi(config);
        }

        public IServiceCollection AddRomi(IServiceCollection services, IConfiguration configuration)
        {
            return services.AddLogging(
                builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Trace);
                        builder.AddNLog(
                                new NLogProviderOptions
                                    {
                                        CaptureMessageTemplates = true, CaptureMessageProperties = true
                                    })
                            .AddConsole().AddDebug();
                    })
                .AddSingleton<RomiDbContext>()
                .AddSingleton(HostSettings.Default);
        }

        public IApplicationBuilder UseRomi(IApplicationBuilder app)
        {
            var settings = app.ApplicationServices.GetService<HostSettings>();
            var configuration = app.ApplicationServices.GetService<IConfiguration>();

            var dbOptions = new DbContextOptionsBuilder<RomiDbContext>().UseSqlite(
                    configuration.GetConnectionString("Romi") ?? HostSettings.Default.ConnectionString,
                    builder => builder.MigrationsAssembly(typeof(RomiDbContext).Assembly.FullName))
                .Options;

            string appPath = configuration["RomiSettings:ApplicationPath"] ?? HostSettings.Default.ApplicationPath;

            string dataPath = configuration["RomiSettings:DataPath"] ?? HostSettings.Default.DataPath;
            if (!Path.IsPathRooted(dataPath))
            {
                dataPath = $"{appPath}/{dataPath}";
            }

            string logPath = configuration["RomiSettings:LogPath"] ?? HostSettings.Default.LogPath;
            if (!Path.IsPathRooted(dataPath))
            {
                logPath = $"{appPath}/{logPath}";
            }

            return app;
        }
    }
}
