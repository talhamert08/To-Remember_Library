using Core.CustomExceptions;
using Web_UI.Models.Utilities.HtppRequestManager.HttpRequestCodes;

namespace Web_UI.Models.Utilities.HtppRequestManager.HttpRequestRouter
{
    public static class RequestRouter
    {
        public static RouterModel Router(int responseStatusCode)
        {
            if (responseStatusCode == RequestCodes.ForBidden)
            {
                return new RouterModel() { Action = "Error", Controller = "Result", Message = BusinessExceptionMessage.Forbidden };
            }
            else if (responseStatusCode == RequestCodes.Unauthorized)
            {
                return new RouterModel() { Action = "Error", Controller = "Result", Message = BusinessExceptionMessage.Unauthorized };
            }
            else
            {
                return new RouterModel() { Action = "Error", Controller = "Result", Message = BusinessExceptionMessage.General };
            }

        }
    }
}
