namespace Starbender.Romi.WebApi.Areas.Device.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Starbender.Romi.WebApi.Models;
    using Starbender.Services.RaspberryPi;

    [ApiController]
    [Produces("application/json")]
    [Route("api/pi")]
    public class PiController : ControllerBase
    {
        private readonly IRaspberryPiService _piService;

        private readonly ILogger _logger;

        private readonly IMapper _mapper;

        public PiController(
            IRaspberryPiService piService,
            ILoggerFactory logFactory,
            IMapper mapper)
        {
            this._piService = piService;
            this._logger = logFactory.CreateLogger<PiController>();
            this._mapper = mapper;
        }

        [Route("ping")]
        [HttpGet]
        public IActionResult Ping()
        {
            this._logger.LogInformation("RaspberryPi Ping...");
            return this.Ok();
        }

        [Route("")]
        [HttpGet]
        public ActionResult GetSystemInfo()
        {
            this._logger.LogInformation("Getting RaspberryPi SystemInfo...");
            var systemInfo = _piService.Info;
            return new JsonResult(systemInfo);
        }

        [Route("pin/{bcmPinNumber}")]
        [HttpGet]
        public ActionResult GetPinState(int bcmPinNumber)
        {
            this._logger.LogInformation("Getting RaspberryPi GetPinState...");
            var pinState = _piService.GpioService.GetPinConfiguration(bcmPinNumber);
            var result = _mapper.Map<GpioPinResponse>(pinState);
            return new JsonResult(result);
        }
    }
}