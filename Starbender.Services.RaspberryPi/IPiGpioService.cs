namespace Starbender.Services.RaspberryPi
{
    using System;
    using System.Threading.Tasks;

    using Starbender.Services.RaspberryPi.Models;
    using Starbender.Services.RaspberryPi.Models.Gpio;

    public interface IPiGpioService
    {
        IGpioPinModel GetPinConfiguration(BcmPinEnum bcmPin);

        IGpioPinModel GetPinConfiguration(int bcmPinNumber);

        Task<bool> HasCapability(IGpioPinModel pin, PinCapabilityEnum capability);

        Task<bool> Read(IGpioPinModel pin);

        Task<int> ReadLevel(IGpioPinModel pin);

        Task<GpioPinValueEnum> ReadValue(IGpioPinModel pin);

        Task RegisterInterruptCallback(IGpioPinModel pin, EdgeDetectionEnum edgeDetection, Action callback);

        Task RegisterInterruptCallback(IGpioPinModel pin, EdgeDetectionEnum edgeDetection, Action<int, int, uint> callback);

        Task<IGpioPinModel> SetPinConfiguration(int bcmPinNumber, IGpioPinModel pinConfiguration);

        Task<IGpioPinModel> SetPinConfiguration(BcmPinEnum bcmPinNumber, IGpioPinModel pinConfiguration);

        Task StartSoftPwm(IGpioPinModel pin, int value, int range);

        Task WaitForValue(IGpioPinModel pin, GpioPinValueEnum status, int timeOutMillisecond);

        Task Write(IGpioPinModel pin, bool value);

        Task Write(IGpioPinModel pin, GpioPinValueEnum value);

        Task Write(IGpioPinModel pin, int value);

        Task WriteLevel(IGpioPinModel pin, int value);
    }
}