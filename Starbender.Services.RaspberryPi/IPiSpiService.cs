namespace Starbender.Services.RaspberryPi
{
    using System.Threading.Tasks;

    using Starbender.Services.RaspberryPi.Models;
    using Starbender.Services.RaspberryPi.Models.Spi;

    public interface IPiSpiService
    {
        /// <summary>
        /// Gets the number of channels on the SPI bus
        /// </summary>
        int ChannelCount { get; }

        /// <summary>
        /// Gets the specified channel
        /// </summary>
        /// <param name="index">The SPI channel index</param>
        /// <returns>The specified channel</returns>
        SpiChannelModel this[int index] { get; }

        /// <summary>
        /// Sends data and simultaneously receives the data in the return buffer.
        /// </summary>
        /// <param name="channel">The SPI channel index</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns>The read bytes from the ring-style bus.</returns>
        Task<byte[]> SendReceive(int channel, byte[] buffer);

        /// <summary>
        /// Writes the specified buffer the the underlying FileDescriptor.
        /// Do not use this method if you expect data back.
        /// This method is efficient if used in a fire-and-forget scenario
        /// like sending data over to those long RGB LED strips.
        /// </summary>
        /// <param name="channel">The SPI channel index</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns>A task performing the write</returns>
        Task Write(int channel, byte[] buffer);
    }
}
