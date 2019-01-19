namespace Starbender.Romi.Web.Service.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.IO;
    using System.Threading;

    using AutoMapper;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;
    using Microsoft.Extensions.Logging;

    using Starbender.Romi.Contracts;
    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;
    using Starbender.Romi.Services.Device;
    using Starbender.Romi.Services.Configuration;
    using Starbender.Romi.Web.Service.Models;
    using Starbender.Services.Gpio.WiringPi;

    using RomiSettings = Starbender.Romi.Data.Models.RomiSettings;


    [ApiController]
    [Produces("application/json")]
    [Route("api/gpio")]
    public class GpioController : ControllerBase
    {
        private readonly IConfigurationService _config;

        private readonly IGpioService _gpioService;

        private readonly ILogger _logger;

        private readonly IMapper _mapper;

        public GpioController(IConfigurationService config, IGpioService gpioService, ILogger<SensorController> logger, IMapper mapper)
        {
            this._config = config;
            this._gpioService = gpioService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [Route("{bcmPinNumber}")]
        [HttpGet]
        public async Task<IActionResult> RegisterSensor(int bcmPinNumber)
        {
            var pinState = await _gpioService.GetPinConfiguration(bcmPinNumber);
            var result = this._mapper.Map<GpioPinResponse>(pinState);
            return new JsonResult(result);
        }
    }
}
