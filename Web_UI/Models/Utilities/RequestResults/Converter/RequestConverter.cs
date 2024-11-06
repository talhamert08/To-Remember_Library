using Core;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Results.IResult;

namespace Web_UI.Models.Utilities.RequestResults.Converter
{

    public static class RequestConverter
    {

        public static IRequestDataResult<T> ConvertData<T>(IRequestDataResult<T> result, int statusCode)
        {

            return new DataRequestResult<T>(result.Data, result.Success, result.Message, statusCode);
        }

        public static IRequestResult ConvertSimple(IRequestResult result, int statusCode)
        {

            return new RequestResult(result.Success, result.Message, statusCode);
        }
    }
}
