using System;

namespace Starbender.Romi.Services.Core
{
    public class ServiceResult
    {
        public static ServiceResult Failure => new ServiceResult(ServiceResultCode.Failure, "An error occured");

        public static ServiceResult Success => new ServiceResult(ServiceResultCode.Success,null);

        public ServiceResult()
        {
            Code = ServiceResultCode.Undefined;
            ErrorMessage = null;
        }

        public ServiceResult(ServiceResultCode code,string errorMessage=null)
        {
            Code = code;
            ErrorMessage = errorMessage;
        }

        public ServiceResultCode Code { get; set; }

        private string ErrorMessage { get; set; }
    }
}
