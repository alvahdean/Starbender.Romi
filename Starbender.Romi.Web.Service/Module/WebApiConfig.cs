namespace Starbender.Romi.Web.Service.Module
{
    using Newtonsoft.Json.Serialization;
    using System.Web.Http;
    using NodaTime.Serialization.JsonNet;
    using NodaTime.TimeZones;

    using Starbender.Core.Time;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { });

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var provider = CurrentTzdbProvider.Load();
            config.Formatters.JsonFormatter.SerializerSettings.ConfigureForNodaTime(provider);
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            //config.Filters.Add(new NoCacheFilter());
        }
    }
}