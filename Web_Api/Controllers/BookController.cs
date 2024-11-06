using Business.Services;
using Core;
using Core.CustomExceptions;
using Core.Utilities.Results;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Authorize] // Güncelleme yaparken gelmemesi için
        [HttpGet("GetSingleBook")]
        public async Task<IDataResult<BookDto>> GetSingleBook(Guid id)
        {
            try
            {

                return new SuccessDataResult<BookDto>(await _bookService.GetByIdAsync(id));

            }
            catch
            {
                return new ErrorDataResult<BookDto>(null, BusinessExceptionMessage.General);
            }
        }

        [HttpGet("GetBooks")]
        public async Task<IDataResult<List<BookDto>>> GetBooks()
        {
            try
            {

                return new SuccessDataResult<List<BookDto>>(await _bookService.GetListAsync());

            }
            catch
            {
                return new ErrorDataResult<List<BookDto>>(null, BusinessExceptionMessage.General);
            }
        }

        [Authorize]
        [HttpPost("AddBook")]
        public async Task<IDataResult<BookDto>> AddBook(BookDto book)
        {
            try
            {
                var res = await _bookService.AddOrUpdateAsync(book);
                return new SuccessDataResult<BookDto>(res);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<BookDto>(null, BusinessExceptionMessage.General);
            }
        }

        [Authorize]
        [HttpPost("UpdateBook")]
        public async Task<IDataResult<BookDto>> UpdateBook(BookDto book)
        {
            try
            {
                return new SuccessDataResult<BookDto>(await _bookService.AddOrUpdateAsync(book));
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<BookDto>(null, BusinessExceptionMessage.General);
            }
        }

        [Authorize(Roles = "advisor")]
        [HttpPost("DeleteBook")]
        public async Task<IResult> DeleteBook(BookDto book)
        {
            try
            {
                await _bookService.DeleteAsync(book);
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult();
            }
        }
    }
}




