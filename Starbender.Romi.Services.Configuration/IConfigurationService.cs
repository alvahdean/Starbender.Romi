namespace Starbender.Romi.Services.Configuration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    using Starbender.Romi.Data.Models;

    public interface IConfigurationService
    {
        IConfiguration AppConfig { get; }

        HostSettings Settings { get; }

        Task DeleteApplicationHost(RomiApplicationHost host);

        Task DeleteApplicationHost(string name);

        Task DeleteDevice(RegisteredDevice deviceInterface);

        Task DeleteInterface(RegisteredInterface deviceInterface);

        Task<TDevice> GetDevice<TDevice>(RomiApplicationHost host = null)
            where TDevice : RegisteredDevice, new();

        Task<List<RegisteredDevice>> GetDevices(
            RomiApplicationHost host = null,
            RegisteredInterface deviceInterface = null);

        Task<RomiApplicationHost> GetHost(string name);

        Task<List<RomiApplicationHost>> GetHosts();

        Task<TInterface> GetInterface<TInterface>()
            where TInterface : RegisteredInterface, new();

        Task<List<RegisteredInterface>> GetInterfaces();

        Task<RomiApplicationHost> GetLocalHost();

        // todo: Move the modification methods to the Administration Service
        Task<RomiApplicationHost> RegisterApplicationHost(string name, HostSettings settings);

        Task<TDevice> RegisterDevice<TDevice>()
            where TDevice : RegisteredDevice, new();

        Task<TInterface> RegisterInterface<TInterface>()
            where TInterface : RegisteredInterface, new();

        Task UpdateApplicationHost(RomiApplicationHost host);
    }
}