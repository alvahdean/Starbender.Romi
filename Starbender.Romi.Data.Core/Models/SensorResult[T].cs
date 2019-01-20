namespace Starbender.Romi.Data.Models
{
    public class SensorResult<TResult> : SensorResult
    {
        public SensorResult()
            : this(null, null, default(TResult))
        {
        }

        public SensorResult(string sensorName, string sensorType, TResult data)
        {
            SensorType = sensorType;
            Attribute = sensorName;
            IsSuccessful = false;
            Exception = null;
            Message = null;
            Result = data;
        }

        public new TResult Result
        {
            get => (TResult)base.Result;
            set => base.Result = value;
        }
    }
}