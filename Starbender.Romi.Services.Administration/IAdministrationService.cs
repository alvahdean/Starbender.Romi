using Starbender.Romi.Data.Models;
using Starbender.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Services.Administration
{
    public interface IAdministrationService
    {
        ServiceResult<RomiSettings> UpdateSettings(RomiSettings settings);

        ServiceResult<RegisteredDevice> AddDevice(RegisteredInterface deviceInterface, RegisteredDevice device);
        ServiceResult RemoveDevice(RegisteredDevice device);

        ServiceResult<RegisteredInterface> AddInterface(RegisteredInterface deviceInterface);
        ServiceResult RemoveInterface(RegisteredInterface deviceInterface);

        ServiceResult<ApplicationIdentity> AddUser(ApplicationIdentity user);
        ServiceResult RemoveUser(ApplicationIdentity user);
        ServiceResult UpdateUser(ApplicationIdentity user);
        ServiceResult<IEnumerable<ApplicationIdentity>> GetUsers();
    }
}