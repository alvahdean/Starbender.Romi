namespace Starbender.Romi.Web.Service.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;

    using Starbender.Core.Data;
    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;
    using Starbender.Romi.Services.Configuration;
    using Starbender.Romi.Web.Service.Models;

    using RomiSettings = Starbender.Romi.Data.Models.RomiSettings;
    
    [ApiController]
    [Produces("application/json")]
    [Route("api/config")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _config;

        public ConfigurationController(IConfigurationService config)
        {
            this._config = config;
        }

        [Route("settings")]
        [HttpGet]
        public async Task<IEnumerable<RomiSettings>> Settings()
        {
            IEnumerable<RomiSettings> result = null;
            using (var uow = new UnitOfWork<RomiSettings>(new RomiDbContext()))
            {
                result = await uow.Repository().AllAsync();
            }

            return result;
        }

        [Route("settings/{hostName}")]
        [HttpGet]
        public async Task<RomiSettings> Settings(string hostName)
        {
            if (hostName.ToUpperInvariant() == "LOCAL")
            {
                hostName = ".";
            }
            var host = await this.GetHost(hostName);
            return host.Settings;
        }

        [Route("hosts/{name}")]
        [HttpPost]
        public async Task<RomiApplicationHost> RegisterHost(string name, [FromBody] Services.Configuration.HostSettings settings = null)
        {
            if (settings == null)
            {
                settings = HostSettings.Default;
            }

            RomiApplicationHost result = await this._config.RegisterApplicationHost(name, settings);

            return result;
        }

        // todo: Implement via service
        [Route("hosts")]
        [HttpGet]
        public async Task<IEnumerable<RomiApplicationHost>> GetHosts()
        {
            return await this._config.GetHosts();
        }

        // todo: Implement via service
        [Route("hosts/{name}")]
        [HttpGet]
        public async Task<RomiApplicationHost> GetHost(string name)
        {
            RomiApplicationHost result = null;
            if (name == "local")
            {
                name = ".";
            }

            using (var uow = new UnitOfWork<RomiApplicationHost>(new RomiDbContext()))
            {
                result = await uow.Repository().SingleOrDefaultAsync(t => t.Name == name);
            }

            return result;
        }

        // todo: Implement via service
        [Route("hosts")]
        [HttpPut]
        public async Task UpdateHost([FromBody] RomiApplicationHost host)
        {
            using (var uow = new UnitOfWork<RomiApplicationHost>(new RomiDbContext()))
            {
                await Task.Run(() => uow.Repository().Update(host));
            }
        }

        // todo: Implement via service
        [Route("hosts")]
        [HttpDelete]
        public async Task DeleteHost([FromBody] RomiApplicationHost host)
        {
            using (var uow = new UnitOfWork<RomiApplicationHost>(new RomiDbContext()))
            {
                await Task.Run(() => uow.Repository().Delete(host));
            }

        }

        [Route("devices")]
        [HttpGet]
        public async Task<IEnumerable<RegisteredDevice>> Devices()
        {
            return await _config.GetDevices();
        }

        [Route("interfaces")]
        [HttpPost]
        public async Task<RegisteredInterface> RegisterInterface([FromBody] RegisteredInterface deviceInterface)
        {
            throw new System.NotImplementedException();
        }

        [Route("interfaces")]
        [HttpGet]
        public async Task<List<RegisteredInterface>> GetInterfaces()
        {
            throw new System.NotImplementedException();
        }


        [Route("interfaces")]
        [HttpDelete]
        public async Task DeleteInterface([FromBody] RegisteredInterface deviceInterface)
        {
            throw new System.NotImplementedException();
        }


        [Route("devices/{host}/register/")]
        [HttpPost]
        public async Task<RegisteredDevice> RegisterDevice(string host, [FromBody] RegisteredDevice device)
        {
            throw new System.NotImplementedException();
        }

        [Route("devices/{host}")]
        public async Task<RomiResponse<List<RegisteredDevice>>> GetDevices(string host)
        {
            throw new System.NotImplementedException();
        }

        [Route("devices")]
        [HttpDelete]
        public async Task<RomiResponse> DeleteDevice([FromBody] RegisteredDevice deviceInterface)
        {
            throw new System.NotImplementedException();
        }
    }
}
