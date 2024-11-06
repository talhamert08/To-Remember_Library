using Core.CustomExceptions;
using Microsoft.AspNetCore.Mvc;

namespace Web_UI.Controllers
{
    public class ResultController : Controller
    {
        public IActionResult Success(string? message)
        {
            if (message != null)
            {
                ViewData["SuccessMessage"] = message;
            }
            else
            {
                ViewData["SuccessMessage"] = BusinessExceptionMessage.Successful;
            }

            return View();
        }

        public IActionResult Error(string? message)
        {
            if (message != null)
            {
                ViewData["ErrorMessage"] = message;
            }
            else
            {
                ViewData["ErrorMessage"] = BusinessExceptionMessage.General;
            }

            return View();
        }

    }
}
