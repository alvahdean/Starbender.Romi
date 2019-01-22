namespace Starbender.Romi.WebApi.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Starbender.Core.Data;
    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;
    using Starbender.Romi.Services.Configuration;

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

        [Route("ping")]
        [HttpGet]
        public IActionResult Ping()
        {
            return this.Ok();
        }

        [Route("devices")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDevice([FromBody] RegisteredDevice deviceInterface)
        {
            throw new NotImplementedException();
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

        [Route("interfaces")]
        [HttpDelete]
        public async Task DeleteInterface([FromBody] RegisteredInterface deviceInterface)
        {
            throw new NotImplementedException();
        }

        [Route("devices")]
        [HttpGet]
        public async Task<IActionResult> Devices()
        {
            return new JsonResult(await this._config.GetDevices());
        }

        [Route("devices/{host}")]
        public async Task<IActionResult> GetDevices(string host)
        {
            throw new NotImplementedException();
        }

        // todo: Implement via service
        [Route("hosts/{name}")]
        [HttpGet]
        public async Task<IActionResult> GetHost(string name)
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

            return new JsonResult(result);
        }

        // todo: Implement via service
        [Route("hosts")]
        [HttpGet]
        public async Task<IActionResult> GetHosts()
        {
            return new JsonResult(await this._config.GetHosts());
        }

        [Route("interfaces")]
        [HttpGet]
        public async Task<IActionResult> GetInterfaces()
        {
            throw new NotImplementedException();
        }

        [Route("devices/{host}/register/")]
        [HttpPost]
        public async Task<IActionResult> RegisterDevice(string host, [FromBody] RegisteredDevice device)
        {
            throw new NotImplementedException();
        }

        [Route("hosts/{name}")]
        [HttpPost]
        public async Task<IActionResult> RegisterHost(string name, [FromBody] HostSettings settings = null)
        {
            if (settings == null)
            {
                settings = HostSettings.Default;
            }

            RomiApplicationHost result = await this._config.RegisterApplicationHost(name, settings);

            return new JsonResult(result);
        }

        [Route("interfaces")]
        [HttpPost]
        public async Task<IActionResult> RegisterInterface([FromBody] RegisteredInterface deviceInterface)
        {
            throw new NotImplementedException();
        }

        [Route("settings")]
        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            IEnumerable<RomiSettings> result = null;
            using (var uow = new UnitOfWork<RomiSettings>(new RomiDbContext()))
            {
                result = await uow.Repository().AllAsync();
            }

            return new JsonResult(result);
        }

        [Route("settings/{hostName}")]
        [HttpGet]
        public async Task<IActionResult> Settings(string hostName)
        {
            if (hostName.ToUpperInvariant() == "LOCAL")
            {
                hostName = ".";
            }

            RomiApplicationHost result;

            using (var uow = new UnitOfWork<RomiApplicationHost>(new RomiDbContext()))
            {
                result = await uow.Repository().SingleOrDefaultAsync(t => t.Name == hostName);
            }
            return new JsonResult(result.Settings);
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
    }
}