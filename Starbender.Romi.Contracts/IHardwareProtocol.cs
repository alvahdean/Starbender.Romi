namespace Starbender.Romi.Contracts
{
    using System.Text;
    using System.Threading.Tasks;

    public interface IHardwareProtocol<in TChannel>
    {
        byte ReadByte(TChannel channel);

        Task<byte> ReadByteAsync(TChannel channel);

        byte[] ReadBytes(TChannel channel, int len);

        Task<byte[]> ReadBytesAsync(TChannel channel, int len);

        string ReadLine(TChannel channel);

        string ReadLine(TChannel channel, Encoding encoding);

        Task<string> ReadLineAsync(TChannel channel);

        Task<string> ReadLineAsync(TChannel channel, Encoding encoding);

        int Write(TChannel channel, byte data);

        int Write(TChannel channel, byte[] data);

        int Write(TChannel channel, string data);

        int Write(TChannel channel, string data, Encoding encoding);

        Task<int> WriteAsync(TChannel channel, byte data);

        Task<int> WriteAsync(TChannel channel, byte[] data);

        Task<int> WriteAsync(TChannel channel, string data);

        Task<int> WriteAsync(TChannel channel, string data, Encoding encoding);
    }
}