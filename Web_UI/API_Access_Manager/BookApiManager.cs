using Core;
using Core.CustomExceptions;
using Core.Entities;
using Core.Utilities.Results;
using Entity.Concrete;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using Web_UI.API_SERVİCE;
using Web_UI.Models.Utilities.RequestResults;
using IResult = Core.Utilities.Results.IResult;

namespace Web_UI.API_Access_Manager
{
    public class BookApiManager
    {
        private readonly ApiManager _manager;

        public BookApiManager(ApiManager manager)
        {
            _manager = manager;
        }

        public async Task<IRequestDataResult<BookDto>> AddBook(BookDto book)
        {
            try
            {
                return await _manager.Post(book, "Book/AddBook");
            }
            catch (Exception ex)
            {
                throw new Exception(message: BusinessExceptionMessage.General);
            }

        }
        public async Task<IRequestDataResult<List<BookDto>>> GetBooks()
        {
            try
            {
                return await _manager.GetList<BookDto>($"Book/GetBooks");
            }
            catch (Exception ex)
            {
                throw new Exception(message: BusinessExceptionMessage.General);
            }
        }

        public async Task<IRequestDataResult<BookDto>> GetSingleBook(Guid id)
        {
            try
            {
                return await _manager.GetSingle<BookDto>($"Book/GetSingleBook?id={id}");
            }
            catch (Exception ex)
            {
                throw new Exception(message: BusinessExceptionMessage.General);
            }
        }

        public async Task<IRequestDataResult<BookDto>> UpdateBook(BookDto book)
        {
            try
            {
                return await _manager.Post<BookDto>(book, "Book/UpdateBook");
            }
            catch (Exception ex)
            {
                throw new Exception(message: BusinessExceptionMessage.General);
            }
        }

        public async Task<IRequestResult> DeleteBook(BookDto book)
        {
            try
            {
                return await _manager.Post<BookDto>(book, "Book/DeleteBook");
            }
            catch (Exception ex)
            {
                throw new Exception(message: BusinessExceptionMessage.General);
            }
        }
    }
}
