using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web_UI.Models.Utilities.RequestResults
{
    public class RequestResult : IRequestResult
    {
        [JsonConstructor]
        public RequestResult(bool success, string message, int requestStatusCode) : this(success)
        {
            Message = message;
            RequestStatusCode = requestStatusCode;
        }
        public RequestResult(bool success)
        {
            Success = success;
        }


        public bool Success { get; }
        public string Message { get; }
        public int RequestStatusCode { get; }
    }
}
