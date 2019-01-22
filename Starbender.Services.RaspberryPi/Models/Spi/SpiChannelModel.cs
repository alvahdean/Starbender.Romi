namespace Starbender.Services.RaspberryPi.Models.Spi
{
    using System;

    using Unosquare.RaspberryIO.Gpio;

    public class SpiChannelModel
    {
        public int MinFrequency { get; internal set; }

        public int MaxFrequency { get; internal set; }

        public int Channel { get; internal set; }

        public int Frequency { get; internal set; }

        internal int FileDescriptor { get; set; }

        internal SpiChannel NativeChannel
        {
            get
            {
                switch (this.Channel)
                {
                    case 0:
                        return Unosquare.RaspberryIO.Pi.Spi.Channel0;

                    case 1:
                        return Unosquare.RaspberryIO.Pi.Spi.Channel1;

                    default:
                        throw new NotSupportedException($"Channel {this.Channel}");
                }
            }
        }
    }
}
