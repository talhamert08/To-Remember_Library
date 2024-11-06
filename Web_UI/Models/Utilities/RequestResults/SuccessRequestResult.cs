using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web_UI.Models.Utilities.RequestResults
{
    public class SuccessRequestResult : RequestResult
    {
        [JsonConstructor]
        public SuccessRequestResult(string message, int requestStatusCode) : base(true, message, requestStatusCode)
        {

        }
        public SuccessRequestResult() : base(true)
        {

        }
    }
}
