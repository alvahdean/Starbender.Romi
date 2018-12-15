using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace Starbender.Romi.Web.Api
{
    public interface IRomiResponse<T> : IRestResponse<T>
    {
    }
}
