using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_UI.Models.Utilities.RequestResults
{
    public class ErrorRequestDataResult<T> : DataRequestResult<T>
    {
        public ErrorRequestDataResult(T data, string message, int requestStatusCode) : base(data, false, message, requestStatusCode)
        {

        }
        public ErrorRequestDataResult(T data) : base(data, false)
        {

        }
        public ErrorRequestDataResult(string message, int requestStatusCode) : base(default, false, message, requestStatusCode)
        {

        }
        public ErrorRequestDataResult() : base(default, false)
        {

        }
    }
}

