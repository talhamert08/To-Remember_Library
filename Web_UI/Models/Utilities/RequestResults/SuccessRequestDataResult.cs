using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web_UI.Models.Utilities.RequestResults
{
    public class SuccessRequestDataResult<T> : DataRequestResult<T>
    {
        [JsonConstructor]
        public SuccessRequestDataResult(T data, string message, int requestStatusCode) : base(data, true, message, requestStatusCode)
        {

        }
        public SuccessRequestDataResult(T data) : base(data, true)
        {

        }
        public SuccessRequestDataResult(string message, int requestStatusCode) : base(default, true, message, requestStatusCode)
        {

        }
        public SuccessRequestDataResult() : base(default, true)
        {

        }
    }
}
