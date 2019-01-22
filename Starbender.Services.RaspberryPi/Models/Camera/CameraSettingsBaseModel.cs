namespace Starbender.Services.RaspberryPi.Models.Camera
{
    public class CameraSettingsBaseModel
    {
        /// <summary>
        /// Gets or sets the timeout milliseconds.
        /// Default value is 5000
        /// Recommended value is at least 300 in order to let the light collectors open.
        /// </summary>
        public int CaptureTimeoutMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to show a preview window on the screen.
        /// </summary>
        public bool CaptureDisplayPreview { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a preview window is shown in full screen  mode if enabled.
        /// </summary>
        public bool CaptureDisplayPreviewInFullScreen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether video stabilization should be enabled.
        /// </summary>
        public bool CaptureVideoStabilizationEnabled { get; set; }

        /// <summary>
        /// Gets or sets the display preview opacity only if the display preview property is enabled.
        /// </summary>
        public byte CaptureDisplayPreviewOpacity { get; set; }

        /// <summary>
        /// Gets or sets the capture sensor region of interest in relative coordinates.
        /// </summary>
        public CameraRectModel CaptureSensorRoi { get; set; }

        /// <summary>
        /// Gets or sets the capture shutter speed in microseconds.
        /// Default -1, Range 0 to 6000000 (equivalent to 6 seconds).
        /// </summary>
        public int CaptureShutterSpeedMicroseconds { get; set; }

        /// <summary>Gets or sets the exposure mode.</summary>
        public CameraExposureModeEnum CaptureExposure { get; set; }

        /// <summary>
        /// Gets or sets the picture EV compensation. Default is 0, Range is -10 to 10
        /// Camera exposure compensation is commonly stated in terms of EV units;
        /// 1 EV is equal to one exposure step (or stop), corresponding to a doubling of exposure.
        /// Exposure can be adjusted by changing either the lens f-number or the exposure time;
        /// which one is changed usually depends on the camera's exposure mode.
        /// </summary>
        public int CaptureExposureCompensation { get; set; }

        /// <summary>Gets or sets the capture metering mode.</summary>
        public CameraMeteringModeEnum CaptureMeteringMode { get; set; }

        /// <summary>
        /// Gets or sets the automatic white balance mode. By default it is set to Auto.
        /// </summary>
        public CameraWhiteBalanceModeEnum CaptureWhiteBalanceControl { get; set; }

        /// <summary>
        /// Gets or sets the capture white balance gain on the blue channel. Example: 1.25
        /// Only takes effect if White balance control is set to off.
        /// Default is 0.
        /// </summary>
        public decimal CaptureWhiteBalanceGainBlue { get; set; }

        /// <summary>
        /// Gets or sets the capture white balance gain on the red channel. Example: 1.75
        /// Only takes effect if White balance control is set to off.
        /// Default is 0.
        /// </summary>
        public decimal CaptureWhiteBalanceGainRed { get; set; }

        /// <summary>
        /// Gets or sets the dynamic range compensation.
        /// DRC changes the images by increasing the range of dark areas, and decreasing the brighter areas. This can improve the image in low light areas.
        /// </summary>
        public CameraDynamicRangeCompensationModel CaptureDynamicRangeCompensation { get; set; }

        /// <summary>
        /// Gets or sets the width of the picture to take.
        /// Less than or equal to 0 in either width or height means maximum resolution available.
        /// </summary>
        public int CaptureWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the picture to take.
        /// Less than or equal to 0 in either width or height means maximum resolution available.
        /// </summary>
        public int CaptureHeight { get; set; }

        /// <summary>
        /// Gets or sets the picture sharpness. Default is 0, Range form -100 to 100.
        /// </summary>
        public int ImageSharpness { get; set; }

        /// <summary>
        /// Gets or sets the picture contrast. Default is 0, Range form -100 to 100.
        /// </summary>
        public int ImageContrast { get; set; }

        /// <summary>
        /// Gets or sets the picture brightness. Default is 50, Range form 0 to 100.
        /// </summary>
        public int ImageBrightness { get; set; }

        /// <summary>
        /// Gets or sets the picture saturation. Default is 0, Range form -100 to 100.
        /// </summary>
        public int ImageSaturation { get; set; }

        /// <summary>
        /// Gets or sets the picture ISO. Default is -1 Range is 100 to 800
        /// The higher the value, the more light the sensor absorbs.
        /// </summary>
        public int ImageIso { get; set; }

        /// <summary>Gets or sets the image capture effect to be applied.</summary>
        public CameraImageEffectModel ImageEffect { get; set; }

        /// <summary>
        /// Gets or sets the color effect U coordinates.
        /// Default is -1, Range is 0 to 255
        /// 128:128 should be effectively a monochrome image.
        /// </summary>
        public int ImageColorEffectU { get; set; }

        /// <summary>
        /// Gets or sets the color effect V coordinates.
        /// Default is -1, Range is 0 to 255
        /// 128:128 should be effectively a monochrome image.
        /// </summary>
        public int ImageColorEffectV { get; set; }

        /// <summary>
        /// Gets or sets the image rotation. Default is no rotation.
        /// </summary>
        public CameraImageRotationModel ImageRotation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image should be flipped horizontally.
        /// </summary>
        public bool ImageFlipHorizontally { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image should be flipped vertically.
        /// </summary>
        public bool ImageFlipVertically { get; set; }

        /// <summary>
        /// Gets or sets the image annotations using a bitmask (or flags) notation.
        /// Apply a bitwise OR to the enumeration to include multiple annotations.
        /// </summary>
        public CameraAnnotationModel ImageAnnotations { get; set; }

        /// <summary>
        /// Gets or sets the image annotations text.
        /// Text may include date/time placeholders by using the '%' character, as used by strftime.
        /// Example: ABC %Y-%m-%d %X will output ABC 2015-10-28 20:09:33.
        /// </summary>
        public string ImageAnnotationsText { get; set; }

        /// <summary>
        /// Gets or sets the font size of the text annotations
        /// Default is -1, range is 6 to 160.
        /// </summary>
        public int ImageAnnotationFontSize { get; set; }

        /// <summary>Gets or sets the color of the text annotations.</summary>
        /// <value>The color of the image annotation font.</value>
        public CameraColorModel ImageAnnotationFontColor { get; set; }

        /// <summary>
        /// Gets or sets the background color for text annotations.
        /// </summary>
        /// <value>The image annotation background.</value>
        public CameraColorModel ImageAnnotationBackground { get; set; }

        /// <summary>Gets the command file executable.</summary>
        public string CommandName { get; }

        /// <summary>Creates the process arguments.</summary>
        /// <returns>The string that represents the process arguments.</returns>
        public string ProcessArguments { get; }
    }
}
