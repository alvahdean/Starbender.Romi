using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi
{
    using AutoMapper;

    public class PiI2CService: IPiI2CService
    {
        private readonly IMapper _mapper;

        public PiI2CService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        // todo: Implement PiI2CService
    }
}
