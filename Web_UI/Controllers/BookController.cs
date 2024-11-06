using Core.CustomExceptions;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Web_UI.API_Access_Manager;
using Web_UI.Models.Utilities.HtppRequestManager.HttpRequestCodes;
using Web_UI.Models.Utilities.HtppRequestManager.HttpRequestRouter;

namespace Web_UI.Controllers
{
    public class BookController : Controller
    {
        private readonly BookApiManager _bookApiManager;
        public BookController(BookApiManager bookApiManager)
        {
            _bookApiManager = bookApiManager;
        }
        public async Task<IActionResult> Index()
        {
            var res = await _bookApiManager.GetBooks();
            return View(res.Data);
        }

        public async Task<IActionResult> DeleteBook(Guid bookId)
        {
            try
            {
                var book = await _bookApiManager.GetSingleBook(bookId);
                var res = await _bookApiManager.DeleteBook(book.Data);
                if (res.RequestStatusCode == RequestCodes.Ok)
                {
                    return RedirectToAction("Success", "Result", new { message = BusinessExceptionMessage.Successful });
                }
                else
                {
                    var router = RequestRouter.Router(res.RequestStatusCode);
                    return RedirectToAction(router.Action, router.Controller, new { message = router.Message });
                }

            }
            catch
            {
                return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
            }
        }


        public async Task<IActionResult> UpdateBook(Guid bookId)
        {
            try
            {
                var res = await _bookApiManager.GetSingleBook(bookId);

                if (res.RequestStatusCode == RequestCodes.Ok)
                {
                    return View(res.Data);

                }
                else
                {
                    var router = RequestRouter.Router(res.RequestStatusCode);
                    return RedirectToAction(router.Action, router.Controller, new { message = router.Message });
                }
            }
            catch
            {
                return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBookMethod(BookDto bookDto)
        {
            try
            {
                var res = await _bookApiManager.UpdateBook(bookDto);

                if (res.RequestStatusCode == RequestCodes.Ok)
                {
                    return RedirectToAction("Success", "Result", new { message = BusinessExceptionMessage.Successful });
                }
                else
                {
                    var router = RequestRouter.Router(res.RequestStatusCode);
                    return RedirectToAction(router.Action, router.Controller, new { message = router.Message });
                }
            }
            catch
            {
                return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
            }

        }

        public async Task<IActionResult> AddBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBookMethod(BookDto bookDto)
        {
            try
            {
                var res = await _bookApiManager.AddBook(bookDto);

                if (res.RequestStatusCode == RequestCodes.Ok)
                {
                    return RedirectToAction("Success", "Result", new { message = BusinessExceptionMessage.Successful });
                }
                else
                {
                    var router = RequestRouter.Router(res.RequestStatusCode);
                    return RedirectToAction(router.Action, router.Controller, new { message = router.Message });
                }
            }
            catch
            {
                return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
            }

        }


    }
}
