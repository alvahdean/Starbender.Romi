using Starbender.Services.RaspberryPi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Starbender.Services.RaspberryPi.Models;
    using Starbender.Services.RaspberryPi.Models.Camera;

    public interface IPiCameraService
    {
        bool IsBusy { get; }

        #region Image Capture Methods

        Task<byte[]> CaptureImageAsync(CameraStillSettingsModel settings, CancellationToken ct);

        byte[] CaptureImage(CameraStillSettingsModel settings);

        Task<byte[]> CaptureImageJpegAsync(int width, int height, CancellationToken ct);

        byte[] CaptureImageJpeg(int width, int height);

        #endregion

        #region Video Capture Methods

        void OpenVideoStream(Action<byte[]> onDataCallback, Action onExitCallback = null);

        void OpenVideoStream(CameraVideoSettingsModel settings, Action<byte[]> onDataCallback, Action onExitCallback);

        void CloseVideoStream();

        #endregion
    }
}
