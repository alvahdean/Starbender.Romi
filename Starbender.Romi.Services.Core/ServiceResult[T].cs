using System;

namespace Starbender.Romi.Services.Core
{
    public class ServiceResult<TResult> : ServiceResult where TResult: class
    {

        public ServiceResult() : this(ServiceResultCode.Failure)
        {
        }

        public ServiceResult(ServiceResultCode code,string errorMessage=null) : base(code,errorMessage)
        {
            Result = null;
        }

        public ServiceResult(TResult result) : this(ServiceResultCode.Success)
        {
            Result = result;
        }
        
        public TResult Result { get; set; }
    }
}
