namespace Starbender.Services.RaspberryPi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.Extensions.Logging;

    using Starbender.Services.RaspberryPi.Config;
    using Starbender.Services.RaspberryPi.Models;
    using Starbender.Services.RaspberryPi.Models.Gpio;

    using Unosquare.RaspberryIO;
    using Unosquare.RaspberryIO.Abstractions;


    public sealed class PiGpioService : IPiGpioService
    {
        private Unosquare.RaspberryIO.Gpio.GpioController _device;

        private IMapper _mapper;

        private readonly ILogger _logger;

        public PiGpioService(IMapper mapper, ILoggerFactory loggerFactory)
        {
            _device = Pi.Gpio;
            _mapper = mapper;
            this._logger = loggerFactory.CreateLogger<PiGpioService>();
        }

        public IGpioPinModel GetPinConfiguration(BcmPinEnum bcmPinEnum)
        {
            return  GetPinConfiguration(BcmPinToNumber(bcmPinEnum));
        }

        public IGpioPinModel GetPinConfiguration(int bcmPinNumber)
        {
            var pinState = _device[bcmPinNumber];
            var model = _mapper.Map<GpioPinModel>(pinState);
            var result = new GpioPinModel();

            return result;
        }

        public async Task<bool> HasCapability(IGpioPinModel pin, PinCapabilityEnum capability)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Read(IGpioPinModel pin)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ReadLevel(IGpioPinModel pin)
        {
            throw new NotImplementedException();
        }

        public async Task<GpioPinValueEnum> ReadValue(IGpioPinModel pin)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterInterruptCallback(IGpioPinModel pin, EdgeDetectionEnum edgeDetection, Action callback)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterInterruptCallback(
            IGpioPinModel pin,
            EdgeDetectionEnum edgeDetection,
            Action<int, int, uint> callback)
        {
            throw new NotImplementedException();
        }

        public async Task<IGpioPinModel> SetPinConfiguration(int bcmPinNumber, IGpioPinModel pinConfiguration)
        {
            throw new NotImplementedException();
        }

        public async Task<IGpioPinModel> SetPinConfiguration(BcmPinEnum bcmPinNumbe, IGpioPinModel pinConfiguration)
        {
            throw new NotImplementedException();
        }

        public async Task StartSoftPwm(IGpioPinModel pin, int value, int range)
        {
            throw new NotImplementedException();
        }

        public async Task WaitForValue(IGpioPinModel pin, GpioPinValueEnum status, int timeOutMillisecond)
        {
            throw new NotImplementedException();
        }

        public async Task Write(IGpioPinModel pin, bool value)
        {
            throw new NotImplementedException();
        }

        public async Task Write(IGpioPinModel pin, GpioPinValueEnum value)
        {
            throw new NotImplementedException();
        }

        public async Task Write(IGpioPinModel pin, int value)
        {
            throw new NotImplementedException();
        }

        public async Task WriteLevel(IGpioPinModel pin, int value)
        {
            throw new NotImplementedException();
        }

        private int BcmPinToNumber(BcmPinEnum bcmPinEnum)
        { 
            return _device.Pins.Where(t => t.Name == bcmPinEnum.ToString()).Select(t => t.BcmPinNumber).First();
        }
    }
}