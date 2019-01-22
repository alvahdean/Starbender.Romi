namespace Starbender.Services.Gpio.WiringPi
{
    using System;
    using System.Threading.Tasks;

    using Starbender.Services.Gpio.WiringPi.Models;

    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.WiringPi;

    public interface IGpioService
    {
        IGpioPinModel GetPinConfiguration(BcmPin bcmPin);

        IGpioPinModel GetPinConfiguration(int bcmPinNumber);

        Task<bool> HasCapability(IGpioPinModel pin, PinCapability capability);

        Task<bool> Read(IGpioPinModel pin);

        Task<int> ReadLevel(IGpioPinModel pin);

        Task<GpioPinValue> ReadValue(IGpioPinModel pin);

        Task RegisterInterruptCallback(IGpioPinModel pin, EdgeDetection edgeDetection, Action callback);

        Task RegisterInterruptCallback(IGpioPinModel pin, EdgeDetection edgeDetection, Action<int, int, uint> callback);

        Task<IGpioPinModel> SetPinConfiguration(int bcmPinNumber, IGpioPinModel pinConfiguration);

        Task<IGpioPinModel> SetPinConfiguration(BcmPin bcmPinNumber, IGpioPinModel pinConfiguration);

        Task StartSoftPwm(IGpioPinModel pin, int value, int range);

        Task WaitForValue(IGpioPinModel pin, GpioPinValue status, int timeOutMillisecond);

        Task Write(IGpioPinModel pin, bool value);

        Task Write(IGpioPinModel pin, GpioPinValue value);

        Task Write(IGpioPinModel pin, int value);

        Task WriteLevel(IGpioPinModel pin, int value);
    }
}