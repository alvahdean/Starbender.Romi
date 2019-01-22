namespace Starbender.Romi.WebApi.Areas.Administration.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    using Starbender.Romi.Data;

    public class ConfigurationModel : PageModel
    {
        private readonly ILogger<ConfigurationModel> _logger;

        private readonly IRomiSettings _settings;

        public ConfigurationModel(ILogger<ConfigurationModel> logger, IRomiSettings settings)
        {
            this._logger = logger;
            this._settings = settings;
            if (this._logger == null)
                throw new ArgumentNullException("logger");
            if (this._settings == null)
            {
                this._logger.LogCritical($"No settings were passed to constructor");
            }

            this._logger.LogDebug("Instance initialized");
        }

        public Dictionary<string, string> Settings { get; private set; }

        public void OnGet()
        {
            this._logger.LogDebug("=> GET Received");
            this.Settings = new Dictionary<string, string>();
            var propList = this.GetProperties(typeof(IRomiSettings));
            this._logger.LogDebug($"Found {propList.Length} properties in settings object");
            foreach (var prop in propList)
            {
                string label = prop.Name;
                object value = prop.GetValue(this._settings);
                string text = string.Empty;
                if (value != null)
                {
                    text = value.GetType().IsClass ? JsonConvert.SerializeObject(value) : value.ToString();
                }

                this.Settings.Add(label, text);
                this._logger.LogDebug($"[{label}]: '{text}'");
            }

            this._logger.LogDebug("<= GET complete");
        }

        private PropertyInfo[] GetProperties(Type type)
        {
            if (type.IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();

                var considered = new List<Type>();
                var queue = new Queue<Type>();
                considered.Add(type);
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces())
                    {
                        if (considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(
                        BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties.Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                return propertyInfos.ToArray();
            }

            return type.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
        }
    }
}