namespace Starbender.Romi.Web.Service.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Starbender.Romi.Services.Configuration;
    using Starbender.Romi.Web.Service.Models;
    using Starbender.Services.Gpio.WiringPi;

    [ApiController]
    [Produces("application/json")]
    [Route("api/gpio")]
    public class GpioController : ControllerBase
    {
        private readonly IConfigurationService _config;

        private readonly IGpioService _gpioService;

        private readonly ILogger _logger;

        private readonly IMapper _mapper;

        public GpioController(
            IConfigurationService config,
            IGpioService gpioService,
            ILogger<SensorController> logger,
            IMapper mapper)
        {
            this._config = config;
            this._gpioService = gpioService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [Route("ping")]
        [HttpGet]
        public IActionResult Ping() => Ok();

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