using System.Text.Json.Serialization;

namespace Web_UI.Models.Utilities.RequestResults
{
    public class DataRequestResult<T> : RequestResult, IRequestDataResult<T>
    {
        [JsonConstructor]
        public DataRequestResult(T data, bool success, string message, int requestStatusCode) : base(success, message, requestStatusCode)
        {
            Data = data;
        }
        public DataRequestResult(T data, bool success) : base(success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}