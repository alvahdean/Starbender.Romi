namespace Starbender.Services.Gpio.WiringPi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Starbender.Services.Gpio.WiringPi.Models;

    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.WiringPi;

    public sealed class GpioService : IGpioService
    {
        private IGpioController _gpioController;

        private IMapper _mapper;

        public GpioService(IMapper mapper)
        {
            this._gpioController = Pi.;
            this._mapper = mapper;
        }

        public IGpioPinModel GetPinConfiguration(BcmPin bcmPin)
        {
            return  GetPinConfiguration(BcmPinToNumber(bcmPin));
        }

        public IGpioPinModel GetPinConfiguration(int bcmPinNumber)
        {
            var pinState = _gpioController[bcmPinNumber];
            var model = this._mapper.Map<GpioPinModel>(pinState);
            var result = new GpioPinModel();

            return result;
        }

        public async Task<bool> HasCapability(IGpioPinModel pin, PinCapability capability)
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

        public async Task<GpioPinValue> ReadValue(IGpioPinModel pin)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterInterruptCallback(IGpioPinModel pin, EdgeDetection edgeDetection, Action callback)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterInterruptCallback(
            IGpioPinModel pin,
            EdgeDetection edgeDetection,
            Action<int, int, uint> callback)
        {
            throw new NotImplementedException();
        }

        public async Task<IGpioPinModel> SetPinConfiguration(int bcmPinNumber, IGpioPinModel pinConfiguration)
        {
            throw new NotImplementedException();
        }

        public async Task<IGpioPinModel> SetPinConfiguration(BcmPin bcmPinNumbe, IGpioPinModel pinConfiguration)
        {
            throw new NotImplementedException();
        }

        public async Task StartSoftPwm(IGpioPinModel pin, int value, int range)
        {
            throw new NotImplementedException();
        }

        public async Task WaitForValue(IGpioPinModel pin, GpioPinValue status, int timeOutMillisecond)
        {
            throw new NotImplementedException();
        }

        public async Task Write(IGpioPinModel pin, bool value)
        {
            throw new NotImplementedException();
        }

        public async Task Write(IGpioPinModel pin, GpioPinValue value)
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

        private int BcmPinToNumber(BcmPin bcmPin) =>
            _gpioController.Where(t => t.BcmPin == bcmPin).Select(t => t.BcmPinNumber).First();

        private BcmPin BcmNumberToPin(int bcmNumber) =>
            _gpioController.Where(t => t.BcmPinNumber == bcmNumber).Select(t => t.BcmPin).First();
    }
}