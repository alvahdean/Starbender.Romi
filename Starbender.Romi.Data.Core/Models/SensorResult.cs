using System;
using System.Collections.Generic;
using System.Text;

namespace Starbender.Romi.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;

    public class SensorResult
    {
        private string _jsonResult;
        private object _result;

        public SensorPoco Sensor { get; set; }

        public string Attribute { get; set; }

        public string SensorType { get; set; }

        public int Id { get; set; }

        [NotMapped]
        public object Result
        {
            get => this._result;
            set
            {
                this._result = value;
                this._jsonResult = value != null ? JsonConvert.SerializeObject(value) : null;
            }
        }

        [IgnoreDataMember]
        public string ResultJson
        {
            get => this._jsonResult;
            set
            {
                this._jsonResult = value;
                this._result = value!=null ? JsonConvert.DeserializeObject(value) : null;
            }
        }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        [IgnoreDataMember]
        public Exception Exception
        {
            set => Message = value.Message;
        }
    }
}
