using Starbender.Romi.Data.Models;
using Starbender.Romi.Services.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Services.Administration
{
    public interface IAdministrationService
    {
        ServiceResult<RomiSettings> GetSettings();
        ServiceResult<RomiSettings> UpdateSettings(RomiSettings settings);

        ServiceResult<SupportedDevice> AddDevice(SupportedInterface deviceInterface, SupportedDevice device);
        ServiceResult RemoveDevice(SupportedDevice device);
        ServiceResult<IEnumerable<SupportedDevice>> GetDevices();

        ServiceResult<SupportedInterface> AddInterface(SupportedInterface deviceInterface);
        ServiceResult RemoveDevice(SupportedInterface deviceInterface);
        ServiceResult<IEnumerable<SupportedInterface>> GetInterfaces();

        ServiceResult<ApplicationIdentity> AddUser(ApplicationIdentity user);
        ServiceResult RemoveUser(ApplicationIdentity user);
        ServiceResult UpdateUser(ApplicationIdentity user);
        ServiceResult<IEnumerable<ApplicationIdentity>> GetUsers();
    }
}