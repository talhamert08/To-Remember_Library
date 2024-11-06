using AutoMapper;
using Core.DataAccess;
using DataAccsess.Concrete.SQL_EntityFrameWork;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class BookManager : ManagerBase<Book, BookDto>, IBookService
    {
        public BookManager(IBookDal dal, IMapper mapper) : base(dal, mapper)
        {
        }
    }
    public interface IBookService : IServiceBase<Book, BookDto>
    {
    }
}
