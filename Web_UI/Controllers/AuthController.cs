using Core.CustomExceptions;
using Core.Entities.Login;
using Microsoft.AspNetCore.Mvc;
using Web_UI.API_Access_Manager;

namespace Web_UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthApiManager _authApiManager;
        public AuthController(AuthApiManager authApiManager)
        {
            _authApiManager = authApiManager;
        }
        public async Task<IActionResult> Login(bool? err)
        {
            ViewData["err"] = err;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userDtoLogin)
        {
            try
            {
                ViewData["err"] = null;
                var x = await _authApiManager.Login(userDtoLogin);
                if (HttpContext.Session.GetString("UserToken") != null)
                {
                    HttpContext.Session.Remove("UserToken");
                    //HttpContext.Session.SetString("UserToken", "");
                }

                if (x.Success)
                {

                    HttpContext.Session.SetString("UserToken", x.Data);

                    return RedirectToAction("Success", "Result", new { message = BusinessExceptionMessage.Successful });

                }
                else if (x.Message == BusinessExceptionMessage.Unauthorized)
                {
                    return RedirectToAction("Login", "Auth", new { err = true });
                }
                else
                {
                    return RedirectToAction("Error", "Result", new { message = x.Message });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Result", new
                {
                    message = BusinessExceptionMessage.General
                });
            }

        }

        public async Task<IActionResult> Logout()
        {

            try
            {

                HttpContext.Session.Remove("UserToken");

                return RedirectToAction("Success", "Result", new { message = BusinessExceptionMessage.Successful });

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
            }

        }

    }
}
