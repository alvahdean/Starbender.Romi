namespace Starbender.Romi.Web.UI
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
//    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

        //"bootstrap": "3.3.6",
        //"jquery": "2.2.0",
        //"jquery-validation": "1.14.0",
        //"jquery-validation-unobtrusive": "3.2.6"

//    using NLog.Extensions.Logging;

    using Starbender.Romi.Data;
//    using Starbender.Romi.Services.Configuration;

    public static class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <param name="env">The hosting environment</param>
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc(
                routes => { routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"); });
        }
    }
}