using System.Collections.Generic;

namespace Starbender.Romi.Services.Configuration
{
    using Starbender.Romi.Data.Models;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    public interface IConfigurationService
    {
        IConfiguration AppConfig { get; }
        HostSettings Settings { get; }

        // todo: Move the modification methods to the Administration Service
        Task<RomiApplicationHost> RegisterApplicationHost(string name, HostSettings settings);
        Task UpdateApplicationHost(RomiApplicationHost host);
        Task DeleteApplicationHost(RomiApplicationHost host);
        Task DeleteApplicationHost(string name);
        Task<TInterface> RegisterInterface<TInterface>() where TInterface : RegisteredInterface, new();
        Task DeleteInterface(RegisteredInterface deviceInterface);
        Task<TDevice> RegisterDevice<TDevice>() where TDevice : RegisteredDevice, new();
        Task DeleteDevice(RegisteredDevice deviceInterface);

        Task<RomiApplicationHost> GetHost(string name);
        Task<List<RomiApplicationHost>> GetHosts();
        Task<RomiApplicationHost> GetLocalHost();
        Task<List<RegisteredInterface>> GetInterfaces();
        Task<TInterface> GetInterface<TInterface>() where TInterface : RegisteredInterface, new();
        Task<List<RegisteredDevice>> GetDevices(RomiApplicationHost host = null, RegisteredInterface deviceInterface = null);
        Task<TDevice> GetDevice<TDevice>(RomiApplicationHost host = null) where TDevice : RegisteredDevice, new();

    }
}

