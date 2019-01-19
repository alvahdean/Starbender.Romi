namespace Starbender.Services.Gpio.WiringPi
{
    using System;
    using System.Threading.Tasks;

    using Starbender.Services.Gpio.WiringPi.Model;

    using Unosquare.RaspberryIO.Abstractions;
    using Unosquare.WiringPi;

    public interface IGpioService
    {
        Task<IGpioPinModel> SetPinConfiguration(int bcmPinNumber, IGpioPinModel pinConfiguration);

        Task<IGpioPinModel> GetPinConfiguration(BcmPin bcmPin);

        Task<IGpioPinModel> GetPinConfiguration(int bcmPinNumber);

        Task<IGpioPinModel> SetPinConfiguration(BcmPin bcmPinNumbe, IGpioPinModel pinConfiguration);

        Task<bool> HasCapability(IGpioPinModel pin, PinCapability capability);

        Task<bool> Read(IGpioPinModel pin);

        Task<int> ReadLevel(IGpioPinModel pin);

        Task<GpioPinValue> ReadValue(IGpioPinModel pin);

        Task RegisterInterruptCallback(IGpioPinModel pin, EdgeDetection edgeDetection, Action callback);

        Task RegisterInterruptCallback(IGpioPinModel pin, EdgeDetection edgeDetection, Action<int, int, uint> callback);

        Task StartSoftPwm(IGpioPinModel pin, int value, int range);

        Task WaitForValue(IGpioPinModel pin, GpioPinValue status, int timeOutMillisecond);

        Task Write(IGpioPinModel pin, bool value);

        Task Write(IGpioPinModel pin, GpioPinValue value);

        Task Write(IGpioPinModel pin, int value);

        Task WriteLevel(IGpioPinModel pin, int value);
    }
}