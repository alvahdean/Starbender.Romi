using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Services.RaspberryPi.Config
{
    using AutoMapper.Mappers;

    internal static class EnumMapExtension
    {
        public static TOut MapByName<TOut>(this Enum src,bool ignoreCase=true) => (TOut)Enum.Parse(typeof(TOut), src.ToString(),ignoreCase);
    }
}
