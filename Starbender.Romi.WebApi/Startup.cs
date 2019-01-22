using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Starbender.Romi.WebApi
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using Autofac;

    using Microsoft.Extensions.Logging;

    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Debug.WriteLine($"**** {nameof(Startup)}");
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Debug.WriteLine($"**** {nameof(ConfigureServices)}");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<RomiDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("Romi")));

            services.AddDefaultIdentity<RomiUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<RomiDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            Debug.WriteLine($"**** {nameof(ConfigureContainer)}");
            // Register Autofac.IModules in this assembly
            var exeAssembly = Assembly.GetExecutingAssembly();
            Debug.WriteLine($"**** Autofac load: {exeAssembly.FullName}");
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            Debug.WriteLine($"**** Autofac load: Starbender.Core.Automapper");
            builder.RegisterAssemblyModules(typeof(Core.Automapper.AutoMapperService));

            // Register Autofac.IModules in all referenced assemblies
            var refAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load);

            foreach (var assembly in refAssemblies)
            {
                Debug.WriteLine($"**** Autofac load: {assembly.FullName}");
                builder.RegisterAssemblyModules(assembly);
            }

            // Load and register Autofac.IModules in all dynamically loaded assemblies
            var modulePath = Configuration.GetValue<string>("RomiSettings:ModulePath");

            if (!string.IsNullOrWhiteSpace(modulePath))
            {
                var moduleFilters = Configuration.GetValue<string[]>("RomiSettings:ModuleFilters");
                LoadDynamicModules(builder, modulePath, moduleFilters);
            }
        }

        public void LoadDynamicModules(ContainerBuilder builder, string moduleDirectory,string[] fileNamePattern=null)
        {
            Debug.WriteLine($"**** {nameof(LoadDynamicModules)}");

            // Make sure process paths are sane...
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            if (!Directory.Exists(moduleDirectory))
            {
                throw new DirectoryNotFoundException(moduleDirectory);
            }

            List<Assembly> assemblies = new List<Assembly>();
            assemblies.AddRange(
                Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.dll", SearchOption.AllDirectories)
                    .Where(filename => fileNamePattern==null || fileNamePattern.Any(pattern => Regex.IsMatch(filename, pattern)))
                    .Select(Assembly.LoadFrom));

            foreach (var assembly in assemblies)
            {
                Debug.WriteLine($"**** Autofac load: {assembly.FullName}");
                builder.RegisterAssemblyTypes(assembly)
                    .AsImplementedInterfaces();
            }

            //// var container = builder.Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Debug.WriteLine($"**** {nameof(Configure)}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
