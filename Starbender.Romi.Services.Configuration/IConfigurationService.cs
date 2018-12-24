using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Services.Configuration
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Diagnostics;

    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;

    public interface IConfigurationService
    {

        Task<RomiApplicationHost> CreateApplicationHost(string name, IRomiSettings settings = null);
        Task<List<RomiApplicationHost>> GetApplicationHosts();
        Task<RomiApplicationHost> GetLocalApplicationHost();
        Task UpdateApplicationHost(RomiApplicationHost host);
        Task DeleteApplicationHost(RomiApplicationHost host);
        Task DeleteApplicationHost(string name);

        Task<IRomiSettings> GetApplicationSettings(RomiApplicationHost host = null);
        Task UpdateApplicationSettings(IRomiSettings settings, RomiApplicationHost host = null);

        
        Task<List<RegisteredInterface>> AddRegisteredInterface(string interfaceName, RomiApplicationHost host);
        Task<List<RegisteredInterface>> GetRegisteredInterfaces(RomiApplicationHost host = null);
        Task<List<RegisteredInterface>> DeleteRegisteredInterface(string interfaceName, RomiApplicationHost host = null);
        Task<List<RegisteredInterface>> DeleteRegisteredInterface(RegisteredInterface deviceInterface, RomiApplicationHost host = null);

        Task<List<RegisteredDevice>> GetRegisteredDevices(RomiApplicationHost host = null, RegisteredInterface deviceInterface=null);

    }
}
