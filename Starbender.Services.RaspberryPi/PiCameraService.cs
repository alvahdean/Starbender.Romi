namespace Starbender.Services.RaspberryPi
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using Starbender.Services.RaspberryPi.Models;
    using Starbender.Services.RaspberryPi.Models.Camera;

    using Unosquare.RaspberryIO;
    using Unosquare.RaspberryIO.Camera;

    public class PiCameraService : IPiCameraService
    {
        private readonly CameraController _device;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public PiCameraService(IMapper mapper, ILoggerFactory loggerFactory)
        {
            _device = Pi.Camera;
            _mapper = mapper;
            this._logger = loggerFactory.CreateLogger<PiCameraService>();
        }

        public bool IsBusy => this._device.IsBusy;

        public bool IsConnected => this._device != null;

        public async Task<byte[]> CaptureImageAsync(CameraStillSettingsModel settings, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public byte[] CaptureImage(CameraStillSettingsModel settings)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> CaptureImageJpegAsync(int width, int height, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public byte[] CaptureImageJpeg(int width, int height)
        {
            throw new NotImplementedException();
        }

        public void OpenVideoStream(Action<byte[]> onDataCallback, Action onExitCallback = null)
        {
            throw new NotImplementedException();
        }

        public void OpenVideoStream(CameraVideoSettingsModel settings, Action<byte[]> onDataCallback, Action onExitCallback)
        {
            throw new NotImplementedException();
        }

        public void CloseVideoStream()
        {
            throw new NotImplementedException();
        }


        private void EnsureConnected()
        {
            if (this._device == null)
            {
                throw new NotSupportedException("No camera attached");
            }
        }
    }
}
