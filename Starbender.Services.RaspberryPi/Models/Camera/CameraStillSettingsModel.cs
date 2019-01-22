namespace Starbender.Services.RaspberryPi.Models.Camera
{
    using System.Collections.Generic;

    using Unosquare.RaspberryIO.Camera;

    public class CameraStillSettingsModel
    {
        /// <summary>
        /// Defines a wrapper for the raspistill program and its settings (command-line arguments).
        /// </summary>
        /// <seealso cref="T:Unosquare.RaspberryIO.Camera.CameraSettingsBase" />
        public class CameraStillSettings : CameraSettingsBase
        {
            /// <inheritdoc />
            public override string CommandName { get; }

            /// <summary>
            /// Gets or sets a value indicating whether the preview window (if enabled) uses native capture resolution
            /// This may slow down preview FPS.
            /// </summary>
            public bool CaptureDisplayPreviewAtResolution { get; set; }

            /// <summary>
            /// Gets or sets the encoding format the hardware will use for the output.
            /// </summary>
            public CameraImageEncodingFormat CaptureEncoding { get; set; }

            /// <summary>
            /// Gets or sets the quality for JPEG only encoding mode.
            /// Value ranges from 0 to 100.
            /// </summary>
            public int CaptureJpegQuality { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the JPEG encoder should add raw bayer metadata.
            /// </summary>
            public bool CaptureJpegIncludeRawBayerMetadata { get; set; }

            /// <summary>
            /// JPEG EXIF data
            /// Keys and values must be already properly escaped. Otherwise the command will fail.
            /// </summary>
            public Dictionary<string, string> CaptureJpegExtendedInfo { get; }

            /// <summary>
            /// Gets or sets a value indicating whether [horizontal flip].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [horizontal flip]; otherwise, <c>false</c>.
            /// </value>
            public bool HorizontalFlip { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [vertical flip].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [vertical flip]; otherwise, <c>false</c>.
            /// </value>
            public bool VerticalFlip { get; set; }

            /// <summary>Gets or sets the rotation.</summary>
            /// <exception cref="T:System.ArgumentOutOfRangeException">Valid range 0-359.</exception>
            public int Rotation { get; set; }
        }
    }
}