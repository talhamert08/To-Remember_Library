using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web_UI.Models.Utilities.RequestResults
{
    public class ErrorRequestResult : RequestResult
    {
        [JsonConstructor]
        public ErrorRequestResult(string message, int requestStatusCode) : base(false, message, requestStatusCode)
        {

        }
        public ErrorRequestResult() : base(false)
        {

        }
    }
}
