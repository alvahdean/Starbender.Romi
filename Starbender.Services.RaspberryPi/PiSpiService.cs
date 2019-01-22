namespace Starbender.Services.RaspberryPi
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Starbender.Services.RaspberryPi.Models;
    using Starbender.Services.RaspberryPi.Models.Spi;

    using Unosquare.RaspberryIO;
    using Unosquare.RaspberryIO.Gpio;

    public class PiSpiService : IPiSpiService
    {
        private List<SpiChannelModel> _channels = new List<SpiChannelModel>();

        private IMapper _mapper;

        public PiSpiService(IMapper mapper)
        {
            _mapper = mapper;
            _channels.Add(_mapper.Map<SpiChannelModel>(Pi.Spi.Channel0));
            _channels.Add(_mapper.Map<SpiChannelModel>(Pi.Spi.Channel1));
        }

        public int ChannelCount => _channels.Count;

        public SpiChannelModel this[int index] => _channels[index];

        public Task<byte[]> SendReceive(int channel, byte[] buffer) =>
            _channels[channel].NativeChannel.SendReceiveAsync(buffer);

        public Task Write(int channel, byte[] buffer) =>
            _channels[channel].NativeChannel.WriteAsync(buffer);
    }
}
