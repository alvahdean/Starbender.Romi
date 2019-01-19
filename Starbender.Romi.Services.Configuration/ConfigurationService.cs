using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbender.Romi.Services.Configuration
{
    using System.Data;
    using System.IO;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using NLog;
    using NLog.Config;
    using NLog.Layouts;
    using NodaTime;
    using NodaTime.Extensions;

    using Starbender.Contracts;
    using Starbender.Core.Data;
    using Starbender.Core.Time;
    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;
    using Starbender.Romi.Services.Device;

    using ILogger = Microsoft.Extensions.Logging.ILogger;

    public class ConfigurationSevice : IConfigurationService
    {
        private readonly ILogger _logger;

        private readonly IMapper _mapper;

        private readonly IExternalTimeZoneProvider _tzProvider;

        public ConfigurationSevice(IConfiguration appConfig, ILogger<ConfigurationSevice> logger,IMapper mapper,RomiSettings settings, IExternalTimeZoneProvider tzProvider)
        {
            this._logger = logger;
            this._mapper = mapper;
            this.Settings = this._mapper.Map<HostSettings>(settings);
            this.AppConfig = appConfig;
            this._tzProvider = tzProvider;
        }

        public IConfiguration AppConfig { get; }

        public HostSettings Settings { get; }

        public void ConfigurePaths(HostSettings settings)
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

        public void ConfigureNLog(HostSettings settings)
        {
            var logConfig = new XmlLoggingConfiguration("NLog.config");

            if (!logConfig.Variables.TryGetValue("logDirectory", out SimpleLayout logDirectory))
            {
                logConfig.Variables.Add("logDirectory", new SimpleLayout(settings.LogPath));
            }
            else
            {
                logConfig.Variables["logDirectory"] = settings.LogPath;
            }
            LogManager.Configuration = logConfig;
        }

        public async Task<RomiApplicationHost> RegisterApplicationHost(string name, HostSettings settings)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = name ?? "[null]";
                throw new DataException($"ApplicationHost name '{name}' is invalid");
            }

            var host= await GetHost(name);
            if (host != null)
            {
                throw  new DataException($"ApplicationHost '{name}' already registered");
            }
            
            host = new RomiApplicationHost() { Name = name, Settings = this._mapper.Map<RomiSettings>(settings), Devices = null };
            using (var uow = new UnitOfWork<RomiApplicationHost,RomiDbContext>())
            {
                await Task.Run(() => uow.Repository().Add(host));
                host = await uow.Repository().Query(x => x.Name == name).Include(t => t.Settings).SingleOrDefaultAsync();
            }

            return host;
        }

        public async Task<List<RomiApplicationHost>> GetHosts()
        {
            List<RomiApplicationHost> result = new List<RomiApplicationHost>();
            using (var uow = new UnitOfWork<RomiApplicationHost, RomiDbContext>())
            {
                result = await uow.Repository().All().Include(t => t.Settings).ToListAsync();
            }

            return result;
        }

        public async Task<RomiApplicationHost> GetHost(string name)
        {
            RomiApplicationHost result = null;
            using (var uow = new UnitOfWork<RomiApplicationHost, RomiDbContext>())
            {
                result = await uow.Repository().Query(x => x.Name == name).Include(t => t.Settings).SingleOrDefaultAsync();
            }

            return result;
        }

        public async Task<RomiApplicationHost> GetLocalHost()
        {
            return await this.GetHost(".");
        }

        public async Task UpdateApplicationHost(RomiApplicationHost host)
        {
            using (var uow = new UnitOfWork<RomiApplicationHost, RomiDbContext>())
            {
                await Task.Run(() => uow.Repository().Update(host));
            }
        }

        public async Task DeleteApplicationHost(RomiApplicationHost host)
        {
            using (var uow = new UnitOfWork<RomiApplicationHost, RomiDbContext>())
            {
                await Task.Run(() => uow.Repository().Delete(host));
            }
        }

        public async Task DeleteApplicationHost(string name)
        {
            var host = await GetHost(name);
            if (host != null)
            {
                await DeleteApplicationHost(host);
            }
        }

        public async Task<TInterface> RegisterInterface<TInterface>()
            where TInterface : RegisteredInterface,new ()
        {
            TInterface devInterface = await this.GetInterface<TInterface>();

            Type type = typeof(TInterface);

            string typeName = type.FullName;

            if (devInterface != null)
            {
                throw new Exception($"DeviceInterface '{typeName}' is already registered");
            }

            var poco = new TInterface() { Name = typeName, };

            using (var uow = new UnitOfWork<RegisteredInterface, RomiDbContext>())
            {
                await Task.Run(() => uow.Repository().Add(poco));
            }

            return await this.GetInterface<TInterface>();
        }

        public async Task<TInterface> GetInterface<TInterface>()
            where TInterface : RegisteredInterface, new()
        {
            Type interfaceType = typeof(TInterface);
            var interfaceList = await this.GetInterfaces();
            return interfaceList.FirstOrDefault(t => t.Name == interfaceType.FullName) as TInterface;
        }

        public async Task<List<RegisteredInterface>> GetInterfaces()
        {
            List < RegisteredInterface > result=new List<RegisteredInterface>();
            using (var uow = new UnitOfWork<RegisteredInterface, RomiDbContext>())
            {
                var items=await uow.Repository().AllAsync();
                result.AddRange(items);
            }

            return result;
        }

        public async Task DeleteInterface(RegisteredInterface deviceInterface)
        {
            using (var uow = new UnitOfWork<RegisteredInterface, RomiDbContext>())
            {
                await Task.Run(() => uow.Repository().Delete(deviceInterface));
            }
        }

        public async Task<TDevice> RegisterDevice<TDevice>() where TDevice : RegisteredDevice, new()
        {
            TDevice device = await this.GetDevice<TDevice>();

            Type type = typeof(TDevice);

            string typeName = type.FullName;

            if (device != null)
            {
                throw new Exception($"Device '{typeName}' is already registered");
            }

            var poco = new TDevice() { Name = typeName, };

            using (var uow = new UnitOfWork<RegisteredDevice, RomiDbContext>())
            {
                await Task.Run(() => uow.Repository().Add(poco));
            }

            return await this.GetDevice<TDevice>();
        }

        public async Task<TDevice> GetDevice<TDevice>(RomiApplicationHost host = null)
            where TDevice : RegisteredDevice, new()
        {
            Type deviceType = typeof(TDevice);
            var deviceList = await this.GetDevices();
            return deviceList.FirstOrDefault(t => t.Name == deviceType.FullName) as TDevice;
        }

        public async Task<List<RegisteredDevice>> GetDevices(
            RomiApplicationHost host = null,
            RegisteredInterface deviceInterface = null)
        {
            if (host == null)
            {
                host = await this.GetLocalHost();
            }

            List<RegisteredDevice> result = new List<RegisteredDevice>();
            using (var uow = new UnitOfWork<RegisteredDevice, RomiDbContext>())
            {
                var items=await uow.Repository().FindAsync(t=>t.ApplicationHost==host && (deviceInterface==null || t.Interface==deviceInterface));
                result.AddRange(items);
            }

            return result;
        }

        public async Task DeleteDevice(RegisteredDevice device)
        {
            if (device != null)
            {
                using (var uow = new UnitOfWork<RegisteredDevice, RomiDbContext>())
                {
                    await Task.Run(() => uow.Repository().Delete(device));
                }
            }
        }
    }
}
