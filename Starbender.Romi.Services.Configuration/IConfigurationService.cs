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
        Task<List<RomiApplicationHost>> GetAppHosts();

        Task<RomiApplicationHost> GetLocalAppHost();

        void Save();

    }
}
