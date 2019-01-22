namespace Starbender.Romi.Services.Administration
{
    using System;
    using System.Collections.Generic;

    using Starbender.Core;
    using Starbender.Romi.Data.Models;

    public interface IAdministrationService
    {
        ServiceResult<RegisteredDevice> AddDevice(RegisteredInterface deviceInterface, RegisteredDevice device);

        ServiceResult<RegisteredInterface> AddInterface(RegisteredInterface deviceInterface);

        ServiceResult<ApplicationIdentity> AddUser(ApplicationIdentity user);

        ServiceResult<IEnumerable<ApplicationIdentity>> GetUsers();

        ServiceResult RemoveDevice(RegisteredDevice device);

        ServiceResult RemoveInterface(RegisteredInterface deviceInterface);

        ServiceResult RemoveUser(ApplicationIdentity user);

        ServiceResult<RomiSettings> UpdateSettings(RomiSettings settings);

        ServiceResult UpdateUser(ApplicationIdentity user);
    }
}