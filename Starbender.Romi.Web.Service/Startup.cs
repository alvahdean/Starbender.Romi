namespace Starbender.Romi.Web.Service
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

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

    using NLog.Extensions.Logging;

    using Starbender.Romi.Data;
    using Starbender.Romi.Services.Configuration;
    using Starbender.Romi.Web.UI;
    using Starbender.Romi.Web.UI.Controllers;
    using Starbender.Romi.Web.UI.Home;
    using Starbender.Romi.Web.UI.Home.Pages;
    using Starbender.Romi.Web.UI.Models;
    using Starbender.Romi.Web.UI.Views;
    using Starbender.Romi.Web.UI.Views.Home;

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
        public RomiSettings RomiSettings { get; private set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">
        /// The services collection to be configured
        /// </param>
        [SuppressMessage(
            "StyleCop.CSharp.ReadabilityRules",
            "SA1101:PrefixLocalCallsWithThis",
            Justification = "Reviewed. Suppression is OK here.")]
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjection.Configure(Configuration, services);

            RomiSettings = DependencyInjection.LoadSettings(Configuration);

            services.AddDbContext<RomiDbContext>(
                options => options.UseSqlite(
                    RomiSettings.ConnectionString,
                    builder => builder.MigrationsAssembly(typeof(Startup).Assembly.FullName)));

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<RomiDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="env">The hosting environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(!string.IsNullOrWhiteSpace(RomiSettings.ApplicationPath))
            {
                Directory.CreateDirectory(RomiSettings.ApplicationPath);
            }
            else
            {
                throw new InvalidDataException("No ROMI application directory configured in AppSettings!");
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<RomiDbContext>();
                context.Database.EnsureCreated();
            }

            Directory.CreateDirectory(RomiSettings.LogPath);
            Directory.CreateDirectory(RomiSettings.DataPath);
            Directory.CreateDirectory(RomiSettings+"/bin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Index");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            UI.Startup.Configure(app,env);

            //app.UseMvc(
            //    routes => { routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"); });

        }
    }
}
