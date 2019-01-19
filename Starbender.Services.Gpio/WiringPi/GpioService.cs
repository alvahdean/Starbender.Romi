namespace Starbender.Services.Gpio.WiringPi
{
    using System;
    using System.Threading.Tasks;

    using Starbender.Services.Gpio.WiringPi.Model;

    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.WiringPi;

    public sealed class GpioService : IGpioService
    {
        public async Task<IGpioPinModel> SetPinConfiguration(int bcmPinNumber, IGpioPinModel pinConfiguration)
        {
            throw new NotImplementedException();
        }

        public async Task<IGpioPinModel> GetPinConfiguration(BcmPin bcmPin)
        {
            throw new NotImplementedException();
        }

        public async Task<IGpioPinModel> GetPinConfiguration(int bcmPinNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<IGpioPinModel> SetPinConfiguration(BcmPin bcmPinNumbe, IGpioPinModel pinConfiguration)
        {
            throw new NotImplementedException();
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

        public async Task RegisterInterruptCallback(IGpioPinModel pin, EdgeDetection edgeDetection, Action<int, int, uint> callback)
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
    }
}