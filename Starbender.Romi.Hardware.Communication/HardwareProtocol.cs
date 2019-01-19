namespace Starbender.Romi.Hardware.Communication
{
    using System.Collections.Generic;
    using System.Text;

    using Starbender.Romi.Contracts;
    using System.Threading.Tasks;

    public abstract class HardwareProtocol<TChannel> : IHardwareProtocol<TChannel>
    {
        public virtual int Write(TChannel channel, byte[] data)
        {
            ValidateChannel(channel);

            int result = 0;

            foreach (byte b in data)
            {
                Write(channel, b);
                result++;
            }

            return result;
        }

        public virtual int Write(TChannel channel, string data) => Write(channel, data, Encoding.UTF8);

        public virtual int Write(TChannel channel, string data, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(data);
            return Write(channel, bytes);
        }

        public abstract int Write(TChannel channel, byte data);

        public abstract byte ReadByte(TChannel channel);

        public virtual byte[] ReadBytes(TChannel channel, int len)
        {
            ValidateChannel(channel);

            List<byte> result=new List<byte>();

            for (int i=0;i<len;i++)
            {
                result.Add(ReadByte(channel));
            }

            return result.ToArray();
        }

        public string ReadLine(TChannel channel)
        {
            return ReadLine(channel, Encoding.UTF8);
        }

        public string ReadLine(TChannel channel, Encoding encoding)
        {
            ValidateChannel(channel);

            List<byte> result = new List<byte>();
            bool done = false;
            while (!done)
            {
                byte data = ReadByte(channel);
                if (data != 0 && data != (byte)'\r' && data != (byte)'\n')
                {
                    result.Add(data);
                }
                else
                {
                    done = true;
                }
            }

            return encoding.GetString(result.ToArray());
        }

        public virtual async Task<int> WriteAsync(TChannel channel, byte data)
        {
            return await Task.Run(() => Write(channel, data));
        }

        public virtual async Task<int> WriteAsync(TChannel channel, byte[] data)
        {
            return await Task.Run(() => Write(channel, data));
        }

        public virtual async Task<int> WriteAsync(TChannel channel, string data)
        {
            return await WriteAsync(channel, data, Encoding.UTF8);
        }

        public virtual async Task<int> WriteAsync(TChannel channel, string data,Encoding encoding)
        {
            return await Task.Run(() => Write(channel, data,encoding));
        }

        public virtual async Task<byte> ReadByteAsync(TChannel channel)
        {
            return await Task.Run(() => ReadByte(channel));
        }

        public virtual async Task<byte[]> ReadBytesAsync(TChannel channel, int len)
        {
            return await Task.Run(() => ReadBytes(channel, len));
        }

        public virtual async Task<string> ReadLineAsync(TChannel channel)
        {
            return await Task.Run(() => ReadLine(channel));
        }

        public virtual async Task<string> ReadLineAsync(TChannel channel,Encoding encoding)
        {
            return await Task.Run(() => ReadLine(channel,encoding));
        }

        protected abstract void ValidateChannel(TChannel channel);
    }
}
