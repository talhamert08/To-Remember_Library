using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_UI.API_Access_Manager;
using Web_UI.API_SERVÝCE;
using Web_UI.Models;

namespace Web_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly BookApiManager _bookApiManager;

        public HomeController(ILogger<HomeController> logger, BookApiManager bookApiManager)
        {
            _logger = logger;
            _bookApiManager = bookApiManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
