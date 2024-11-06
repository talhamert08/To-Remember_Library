using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_UI.Models.Utilities.RequestResults
{
    public interface IRequestResult
    {
        bool Success { get; }
        string Message { get; }
        int RequestStatusCode { get; }

    }
}
